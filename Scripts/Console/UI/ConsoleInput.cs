using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Console.UI {

    public class ConsoleInput : MonoBehaviour {

        [Header("Inputs")]
        [SerializeField, Tooltip("Which key stroke traverses through the history forward?")]
        private KeyCode forwardHistoryKey = KeyCode.UpArrow;
        [SerializeField, Tooltip("Which key stroke traverses through the history backward?")]
        private KeyCode backwardHistoryKey = KeyCode.DownArrow;

        [Header("UI Fields")]
        [SerializeField, Tooltip("What is the text input field to reference?")]
        private InputField inputField;

        [Header("History Storage")]
        [SerializeField, Tooltip("Which scriptable object store the command history?")]
        private CommandHistory commandHistory;

        private void Start() {
            Assert.IsNotNull(inputField, "No command input field cached!");
            Assert.IsNotNull(commandHistory, "No command history cached!");
        }

        private void Update() {
            if (Input.GetKeyUp(forwardHistoryKey)) {
                commandHistory.DecrementHistory();
                var command = commandHistory.GetRecentCommand();
                SetInputFieldText(command);
            } else if (Input.GetKeyUp(backwardHistoryKey)) {
                commandHistory.IncrementHistory();
                var command = commandHistory.GetRecentCommand();
                SetInputFieldText(command);
            }
        }

        private void SetInputFieldText(string input) {
            inputField.text = input;
        }

        /// <summary>
        /// Sets the input field to be empty. The input parameter
        /// is not needed, but this is used for UnityEvents.
        /// </summary>
        /// <param name="input">The input from the input field.</param>
        public void SetEmptyField(string input) {
            inputField.text = string.Empty;
        }
    }
}
