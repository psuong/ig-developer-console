using DeveloperConsole;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;

namespace DeveloperConsole.UI {

    public class ConsoleOutputUIManager : MonoBehaviour {

        [Header("UI")]
        [SerializeField, Tooltip("Which transform should the outputTextTemplates instantiate under?")]
        private Transform textOutputParent;
        [SerializeField, Tooltip("Which text field will store the outputs of the console?")]
        private Text outputTextTemplate;
        
        [Header("Output Storage")]
        [SerializeField, Tooltip("Which scriptable object stored the value of the console output?")]
        private ConsoleOutputStorage consoleOutputStorage;

        private IList<Text> consoleOutputs;

        private void Start() {
            Assert.IsNotNull(outputTextTemplate, "No output text template cached!");
            Assert.IsNotNull(textOutputParent, "No parent transform cached!");
            Assert.IsNotNull(consoleOutputStorage, "No console output cached!");
        }

        private void CreateOutputMessage(string message, Color textColor) {
            var output = Instantiate(outputTextTemplate) as Text;
            output.text = message;
            output.color = textColor;
        }

        private void AddConsoleOutput(Text text) {
            if (consoleOutputs.Count < consoleOutputStorage.ConsoleOutputHistorySize) {
            }
        }
    }
}
