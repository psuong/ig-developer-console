using DeveloperConsole;
using GlobalEvents;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

namespace DeveloperConsole.UI {

    using DeveloperConsole.Utility;

    public class ConsoleOutputUIManager : MonoBehaviour {

        [Header("UI")]
        [SerializeField, Tooltip("Which transform should the outputTextTemplates instantiate under?")]
        private Transform textOutputParent;
        [SerializeField, Tooltip("Which text field will store the outputs of the console?")]
        private Text outputTextTemplate;

        [Header("Output Storage")]
        [SerializeField]
        private int historySize = 20;

        private IList<Tuple<string, Color, Text>> consoleOutputs;

        private void OnEnable() {
            GlobalEventHandler.SubscribeEvent<string, Color>(ConsoleEventConstants.AddOutputEventName, AddConsoleOutput);
        }

        private void OnDisable() {
            GlobalEventHandler.UnsubscribeEvent<string, Color>(ConsoleEventConstants.AddOutputEventName, AddConsoleOutput);
        }

        private void Start() {
            consoleOutputs = new List<Tuple<string, Color, Text>>();
            Assert.IsNotNull(outputTextTemplate, "No output text template cached!");
            Assert.IsNotNull(textOutputParent, "No parent transform cached!");
        }

        private void AddConsoleOutput(string message, Color color) {
            Text text = CreateOutputMessage(message, color);
            consoleOutputs.Add(Tuple<string, Color, Text>.Create(message, color, text));
            if (consoleOutputs.Count > historySize) {
                consoleOutputs.RemoveAt(0);
            }
        }
        private Text CreateOutputMessage(string message, Color textColor) {
            var output = Instantiate(outputTextTemplate, textOutputParent);
            output.text = message;
            output.color = textColor;
            return output;
        }

    }
}
