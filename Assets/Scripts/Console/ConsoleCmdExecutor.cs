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

        private void SafeInvoke(string argv) {
            var args = argv.Split(delimiter);
            var eventName = args[0];

            switch(args.Length) {
                case 1:
                    GlobalEventHandler.InvokeEvent(eventName);
                    return;
                case 2:
                    GlobalEventHandler.InvokeEvent(eventName, argParser.GetArgValue(args[1]));
                    return;
                case 3:
                    GlobalEventHandler.InvokeEvent(eventName, argParser.GetArgValue(args[1]), argParser.GetArgValue(args[2]));
                    return;
                case 4:
                    GlobalEventHandler.InvokeEvent(eventName, argParser.GetArgValue(args[1]), argParser.GetArgValue(args[2]), argParser.GetArgValue(args[3]));
                    return;
                case 5:
                    GlobalEventHandler.InvokeEvent(eventName, argParser.GetArgValue(args[1]), argParser.GetArgValue(args[2]), argParser.GetArgValue(args[3]));
                    return;
                case 6:
                    GlobalEventHandler.InvokeEvent(eventName, argParser.GetArgValue(args[1]), argParser.GetArgValue(args[2]), argParser.GetArgValue(args[3]), argParser.GetArgValue(args[4]));
                    return;
                
            }
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
