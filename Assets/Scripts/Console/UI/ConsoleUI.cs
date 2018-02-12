using UnityEngine;

namespace DeveloperConsole.UI {
    
    [RequireComponent(typeof(CanvasGroup))]
    public class ConsoleUI : MonoBehaviour {

        [Header("Inputs")]
        [SerializeField, Tooltip("What key should be pressed to enable/disable the command?")]
        private KeyCode showConsoleKey = KeyCode.BackQuote;

        private CanvasGroup canvasGroup;
        private bool isConsoleShowing;

        private void Start() {
            canvasGroup = GetComponent<CanvasGroup>();

            // Disable the console at Start
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
        }

        private void DisableConsole() {
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = canvasGroup.interactable = isConsoleShowing = false;
        }

        private void EnableConsole() {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = canvasGroup.interactable = isConsoleShowing = true;
        }

    }
}
