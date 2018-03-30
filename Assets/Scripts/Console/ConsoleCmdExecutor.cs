using UnityEngine;
using System;

namespace DeveloperConsole {

    using GlobalEvents;

    public class ConsoleCmdExecutor : MonoBehaviour {
        
        [SerializeField, Tooltip("The character used to split the input, by default a space is used.")]
        private char delimiter = ' ';

        private ArgParser argParser;

        private void Awake() {
            argParser = new ArgParser();
        }

        private void SafeInvoke(string arg) {
            // TODO: Figure out a way to safely invoke global events.
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Executes a delegate within the global event table if it exists.
        /// </summary>
        /// <param name="input">The user's input</param>
        public void TryExecuteCommand(string input) {
            string trimmedInput = input.Trim();
            SafeInvoke(trimmedInput);
        }
    }
}
