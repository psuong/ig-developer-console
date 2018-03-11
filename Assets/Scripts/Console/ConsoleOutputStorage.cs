using System.Collections.Generic;
using UnityEngine;

namespace DeveloperConsole {

    using DeveloperConsole.Utility;

    [CreateAssetMenu(fileName = "ConsoleOutputStorage", menuName = "Developer Console/ConsoleOutputStorage")]
    public class ConsoleOutputStorage : ScriptableObject {

        /// <summary>
        /// Returns a list of the console outputs.
        /// </summary>
        public IList<Tuple<string, Color>> ConsoleOutputs {
            get { return consoleOutputs; }
        }
        
        /// <summary>
        /// Returns the # of elements that can be saved.
        /// </summary>
        public int HistorySize {
            get { return historySize; }
        }

        [SerializeField, Tooltip("How many outputs should be stored?")]
        private int historySize = 20;

        private IList<Tuple<string, Color>> consoleOutputs;

        private void OnEnable() {
            consoleOutputs = new List<Tuple<string, Color>>();

            // Subscribe the event
            GlobalEvents.GlobalEventHandler.SubscribeEvent<string, Color>(ConsoleEventConstants.AddOutputEventName, AddConsoleOutput);
        }

        private void OnDisable() {
            // Unsubscribe the event on disable
            GlobalEvents.GlobalEventHandler.SubscribeEvent<string, Color>(ConsoleEventConstants.AddOutputEventName, AddConsoleOutput);
        }

        /// <summary>
        /// Adds a non empty message to the console output.
        /// </summary>
        /// <param name="message">The output message to store.</param>
        /// <param name="color">The color of the output message.</param>
        public void AddConsoleOutput(string message, Color color) {
            if (message != string.Empty) {
                var output = Tuple<string, Color>.Create(message, color);
                if (consoleOutputs.Count < historySize) {
                    consoleOutputs.Add(output);
                } else {
                    consoleOutputs.RemoveAt(0);
                    consoleOutputs.Add(output);
                }
            }
        }
    }
}
