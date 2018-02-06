using UnityEngine;

namespace DeveloperConsole {
    using GlobalEvents;
    
    public class ConsoleCmdExecutor : MonoBehaviour {

        /// <summary>
        /// Executes a delegate within the global event table if it exists.
        /// </summary>
        /// <param name="input">The user's input</param>
        public void TryExecuteCommand(string input) {
            if (GlobalEventHandler.IsEventRegistered (input)) {
                // TODO: Log the output of the event
                GlobalEventHandler.InvokeEvent (input);
            } else {
                // TODO: Log the output of a non registered event
                Debug.LogWarningFormat("No event registered with the ID: {0}", input);
            }
        }

    }
}
