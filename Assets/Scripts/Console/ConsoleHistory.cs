using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeveloperConsole {

    [CreateAssetMenu(fileName = "ConsoleHistory", menuName = "Developer Console/Console History")]
    public class ConsoleHistory : ScriptableObject {

        [SerializeField, Tooltip("How many commands should we keep?")]
        private int historySize = 20;

        private int currentIndex;               // Stores the current index of what's added to the history
        private IList<string> consoleHistory;   // Stores the console cmds

        private void OnEnable() {
            consoleHistory = new List<string>();
            currentIndex = 0;
        }
        
        /// <summary>
        /// Records non empty string console commands.
        /// </summary>
        /// <param name="cmd">A string entered in the input field.</param>
        public void AddConsoleCmd(string cmd) {
            if (cmd != string.Empty) {
                if (consoleHistory.Count < historySize) {
                    consoleHistory.Add(cmd);
                } else {
                    // Treat the list like a stack-queue
                    consoleHistory.RemoveAt(0);
                    consoleHistory.Add(cmd);
                }
            }
            currentIndex = consoleHistory.Count;
        }
        
        /// <summary>
        /// Infinitely increments through the console history. When the index + 1
        /// is greater than the history size, it resets the index back to 0.
        /// </summary>
        /// <returns>The newly computed index.</returns>
        public int IncrementHistory() {
            var size = consoleHistory.Count;
            if (size > 0) {
                currentIndex = (currentIndex + 1) % size;
            }
            return currentIndex;
        }

        /// <summary>
        /// Infinitely decrements through the console history. When the index - 1
        /// is less than zero, it resets the index to the end of the console history.
        /// </summary>
        /// <returns>The newly computed index.</returns>
        public int DecrementHistory() {
            var size = consoleHistory.Count;
            if (size > 0) {
                currentIndex = (currentIndex - 1 < 0) ? size - 1 : (currentIndex - 1) % size;
            }
            return currentIndex;
        }
        
        /// <summary>
        /// Retrieves the current console command.
        /// </summary>
        /// <returns>A non empty string if the console command.</returns>
        public string GetRecentCommand() {
            if(consoleHistory.Count > 0) {
                return consoleHistory[currentIndex];
            } else {
                return string.Empty;
            }
        }

    }

}
