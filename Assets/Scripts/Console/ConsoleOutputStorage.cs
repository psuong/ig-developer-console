using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeveloperConsole {

    [CreateAssetMenu(fileName = "ConsoleOutputStorage", menuName = "DeveloperConsole/ConsoleOutputStorage")]
    public class ConsoleOutputStorage : ScriptableObject {
        
        /// <summary>
        /// Returns an array of the console outputs.
        /// </summary>
        public string[] ConsoleOutputs {
            get { return consoleOutputs.ToArray(); }
        }

        [SerializeField, Tooltip("How many outputs should be stored?")]
        private int consoleOutputHistorySize = 20;

        private List<string> consoleOutputs;

        private void OnEnable() {
            consoleOutputs = new List<string>();
        }

        /// <summary>
        /// Adds a non empty message to the console output.
        /// </summary>
        public void AddConsoleOutput(string message) {
            if (message != string.Empty) {
                if (consoleOutputs.Count < consoleOutputHistorySize) {
                    consoleOutputs.Add(message);
                } else {
                    consoleOutputs.RemoveAt(0);
                    consoleOutputs.Add(message);
                }
            }
        }

    }
}
