using System.Collections.Generic;
using UnityEngine;

namespace Console {

    [CreateAssetMenu(fileName = "CommandHistory", menuName = "Developer Console/Command History")]
    public class CommandHistory : ScriptableObject {

        [SerializeField, Tooltip("How many commands should we keep?")]
        private int cmdHistorySize = 20;

        private int currentIndex;               // Stores the current index of what's added to the history
        private IList<string> commandHistory;   // Stores the console cmds

        private void OnEnable() {
            commandHistory = new List<string>();
            currentIndex = 0;
        }

        private bool IsPreviousCurrent(string cmd) {
            return (commandHistory.Count > 0) ? commandHistory[commandHistory.Count - 1] == cmd : false;
        }
        
        /// <summary>
        /// Adds a unique console command.
        /// </summary>
        private void TryAddCommand(string cmd) {
            if (!IsPreviousCurrent(cmd)) {
                commandHistory.Add(cmd);
            }
        }
        
        /// <summary>
        /// Records non empty string console commands.
        /// </summary>
        /// <param name="cmd">A string entered in the input field.</param>
        public void AddConsoleCmd(string cmd) {
            if (cmd != string.Empty) {
                if (commandHistory.Count < cmdHistorySize) {
                    TryAddCommand(cmd);
                } else {
                    // Treat the list like a stack-queue
                    commandHistory.RemoveAt(0);
                    commandHistory.Add(cmd);
                }
            }
            currentIndex = commandHistory.Count;
        }
        
        /// <summary>
        /// Infinitely increments through the console history. When the index + 1
        /// is greater than the history size, it resets the index back to 0.
        /// </summary>
        /// <returns>The newly computed index.</returns>
        public int IncrementHistory() {
            var size = commandHistory.Count;
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
            var size = commandHistory.Count;
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
            return (commandHistory.Count > 0) ? commandHistory[currentIndex] : string.Empty;
        }
    }
}
