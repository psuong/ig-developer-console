using Console.UI;
using GlobalEvents;
using UnityEngine;

namespace Console.Demo {
    
    /**
     * This class just pauses the game, nothing really special here.
     */
    public class PauseHandler : MonoBehaviour {

        private float timeScale;

        private void Awake() {
            timeScale = Time.timeScale;
        }

        private void OnEnable() {
            GlobalEventHandler.SubscribeEvent("Pause", Pause);
            GlobalEventHandler.SubscribeEvent("Resume", Resume);
        }

        private void OnDisable() {
            GlobalEventHandler.UnsubscribeEvent("Pause", Pause);
            GlobalEventHandler.UnsubscribeEvent("Resume", Resume);
        }

        private void Pause() {
            Time.timeScale = 0f;
            ConsoleOutput.Log("Paused!", Color.red);
        }

        private void Resume() {
            Time.timeScale = timeScale;
            ConsoleOutput.Log("Resumed!", Color.yellow);
        }
    }
}
