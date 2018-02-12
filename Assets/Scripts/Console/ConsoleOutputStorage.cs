using System.Collections.Generic;
using UnityEngine;

namespace DeveloperConsole {

    [CreateAssetMenu(fileName = "ConsoleOutputStorage", menuName = "Developer Console/ConsoleOutputStorage")]
    public class ConsoleOutputStorage : ScriptableObject {
        
        /// <summary>
        /// Returns a list of the console outputs.
        /// </summary>
        public IList<string> ConsoleOutputs {
            get { return consoleOutputs; }
        }
        
        /// <summary>
        /// Returns the # of elements that can be saved.
        /// </summary>
        public int ConsoleOutputHistorySize {
            get { return consoleOutputHistorySize; }
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
        /// <param name="message">The output message to store.</param>
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
