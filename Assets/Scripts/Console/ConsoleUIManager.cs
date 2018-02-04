using UnityEngine;
using UnityEngine.UI;

namespace DeveloperConsole.UI {

    [RequireComponent (typeof (CanvasGroup))]
    public class ConsoleUIManager : MonoBehaviour {

        [Header ("Canvas Properties")]
        [SerializeField, Range (0f, 1f)]
        private float startAlpha = 0f;
        [SerializeField, Range (0f, 1f)]
        private float endAlpha = 1f;
        [SerializeField, Tooltip ("What key should be pressed to enable/disable the console?")]
        private KeyCode inputKey = KeyCode.BackQuote;

        private CanvasGroup canvasGroup;
        private bool isConsoleShowing;

        private void Awake () {
            isConsoleShowing = false;
        }

        private void Start () {
            canvasGroup = GetComponent<CanvasGroup> ();
            // Disable the canvas by default
            DisableConsole ();
        }

        private void Update () {
            if (Input.GetKeyUp (inputKey)) {
                if (!isConsoleShowing) {
                    EnableConsole ();
                } else {
                    DisableConsole ();
                }
            }
        }

        private void DisableConsole () {
            canvasGroup.alpha = startAlpha;
            canvasGroup.blocksRaycasts = canvasGroup.interactable = isConsoleShowing = false;
        }

        private void EnableConsole () {
            canvasGroup.alpha = endAlpha;
            canvasGroup.blocksRaycasts = canvasGroup.interactable = isConsoleShowing = true;
        }

    }
}
