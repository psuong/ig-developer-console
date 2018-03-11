using UnityEngine;

namespace DeveloperConsole.UI {
    
    public class ConsoleUI : MonoBehaviour {

        [Header("Inputs")]
        [SerializeField, Tooltip("What key should be pressed to enable/disable the command?")]
        private KeyCode showConsoleKey = KeyCode.BackQuote;

        [Header("UI")]
        [SerializeField, Tooltip("Which Developr Console should be managed?")]
        private GameObject developerConsole;

        private bool isConsoleShowing;

        private void Awake() {
            // Disable the Console immediately.
            SetConsoleState(false);
        }

        private void Update() {
            if(Input.GetKeyUp(showConsoleKey)) {
                if(!isConsoleShowing) {
                    SetConsoleState(true);
                } else {
                    SetConsoleState(false);
                }
            }
        }

        private void SetConsoleState(bool isShowing) {
            developerConsole.active = isConsoleShowing = isShowing;
        }
    }
}
