using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace DeveloperConsole.UI {

    [RequireComponent(typeof(CanvasGroup))]
    public class ConsoleUIManager : MonoBehaviour {
        
        [Header("Inputs")]
        [SerializeField, Tooltip("What key should be pressed to enable/disable the console?")]
        private KeyCode showConsoleKey = KeyCode.BackQuote;
        [SerializeField]
        private KeyCode forwardHistoryKey = KeyCode.UpArrow;
        [SerializeField]
        private KeyCode backwardHistoryKey = KeyCode.DownArrow;

        [Header("UI Property")]
        [SerializeField, Tooltip("What is the text input field to reference?")]
        private InputField inputField;

        [Header("Console History Storage")]
        [SerializeField, Tooltip("Which scriptable object store the console history?")]
        private ConsoleHistory consoleHistory;

        private CanvasGroup canvasGroup;
        private bool isConsoleShowing;

        private void Awake() {
            isConsoleShowing = false;
        }

        private void Start() {
            canvasGroup = GetComponent<CanvasGroup>();

            Assert.IsNotNull(inputField, "No console input field cached!");
            Assert.IsNotNull(consoleHistory, "No console history cached!");

            // Disable the canvas by default
            DisableConsole();
        }

        private void Update() {
            if(Input.GetKeyUp(showConsoleKey)) {
                if(!isConsoleShowing) {
                    EnableConsole();
                } else {
                    DisableConsole();
                }
            }

            if (Input.GetKeyUp(forwardHistoryKey)) {
                consoleHistory.DecrementHistory();
                var command = consoleHistory.GetRecentCommand();
                SetInputFieldText(command);
            }

            if (Input.GetKeyUp(backwardHistoryKey)) {
                consoleHistory.IncrementHistory();
                var command = consoleHistory.GetRecentCommand();
                SetInputFieldText(command);
            }
        }

        private void DisableConsole() {
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = canvasGroup.interactable = isConsoleShowing = false;
        }

        private void EnableConsole() {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = canvasGroup.interactable = isConsoleShowing = true;
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
