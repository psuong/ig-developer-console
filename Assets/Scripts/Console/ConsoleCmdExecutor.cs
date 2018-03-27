using UnityEngine;

namespace DeveloperConsole {

    using GlobalEvents;

    public class ConsoleCmdExecutor : MonoBehaviour {
        
        [SerializeField, Tooltip("The character used to split the input, by default a space is used.")]
        private char delimiter = ' ';

        private ArgParser argParser;

        private void Awake() {
            argParser = new ArgParser();
        }

        private string[] GetArguments(string input) {
            return input.Split(delimiter);
        }

        private void SafeInvoke(string[] args, int argLength) {
            // TODO: Implement a SafeInvoke call to execute events
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Executes a delegate within the global event table if it exists.
        /// </summary>
        /// <param name="input">The user's input</param>
        public void TryExecuteCommand(string input) {
            string trimmedInput = input.Trim();
            // TODO: Swap the direct call with a safe invoke call
            if (GlobalEventHandler.IsEventSubscribed(trimmedInput)) {
                GlobalEventHandler.InvokeEvent(input);
            } 
#if UNITY_EDITOR
            else {
                Debug.LogWarningFormat("No event registered with the ID: {0}", input);
            }
#endif
        }
    }
}
