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
        [SerializeField, Tooltip("How many outputs can be logged to the console?")]
        private int historySize = 20;

        private IList<Tuple<string, Color, Text>> consoleOutputs;

        private void OnEnable() {
            // Subscribe the AddConsoleOutput to the Event Handler
            GlobalEventHandler.SubscribeEvent<string, Color>(ConsoleEventConstants.AddOutputEventName, AddConsoleOutput);
        }

        private void OnDisable() {
            // Unsubscribe the AddConsoleOutput from the Event Handler
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
            output.color = textColor;
            output.text = string.Format("#{0}. {1}", consoleOutputs.Count + 1, message);
            return output;
        }

    }
}
