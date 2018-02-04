using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DeveloperConsole {
   
    [CreateAssetMenu (fileName = "ConsoleHistory", menuName = "Developer Console/Console History")]
    public class ConsoleHistory : ScriptableObject {

        [SerializeField, Tooltip ("How many commands should we keep?")]
        private int historySize = 20;

        private int currentIndex;               // Stores the current index of what's added to the history
        private IList<string> consoleHistory;   // Stores the console cmds

        private void OnEnable () {
            consoleHistory = new List<string> ();
            currentIndex = 0;
        }

        public void AddConsoleCmd (string cmd) {
            if (consoleHistory.Count < historySize) {
                consoleHistory.Add (cmd);
            } else {
                // Treat the list like a stack-queue
                consoleHistory.RemoveAt (0);
                consoleHistory.Add (cmd);
            }
        }

        public int IncrementHistory() {
            return Mathf.Clamp (currentIndex++, 0, historySize);
        }

        public int DecrementHistory() {
            return Mathf.Clamp (currentIndex--, 0, historySize);
        }

        public string GetRecentCommand () {
            if (consoleHistory.Count > 0) {
                return consoleHistory[currentIndex];
            } else {
                return string.Empty;
            }
        }

    }

}
