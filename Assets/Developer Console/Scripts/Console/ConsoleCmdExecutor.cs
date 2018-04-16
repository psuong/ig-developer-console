using UnityEngine;
using UnityEngine.Assertions;

namespace Console {

    using GlobalEvents;

    public class ConsoleCmdExecutor : MonoBehaviour {

        [SerializeField, Tooltip("What is the separator of the command line input? By default it is a space character.")]
        private char delimiter = ' ';

        [SerializeField, Tooltip("Which scriptable object stores the instance id, object pair?")]
        private IdCache cache;

        [SerializeField, Tooltip("Should the regex be user defined?")]
        private bool useCustomRegexPatterns;

        [SerializeField, HideInInspector]
        private string[] customRegexPatterns = new string[] {
            @"s",
            @"^{1}$",
            @"^\d$",
            @"^[0-9]*(?:\.[0-9]*)?$",
            @"^.+"
        };

        private ArgParser argParser;

        private void Awake() {
            argParser = (useCustomRegexPatterns) ? 
                new ArgParser(
                    customRegexPatterns[0],
                    customRegexPatterns[1],
                    customRegexPatterns[2],
                    customRegexPatterns[3],
                    customRegexPatterns[4]
                ) : new ArgParser();
            Assert.IsNotNull(cache, "No IdCache was found!");
        }

        private string[] CopyArgs(string[] args, int startIndex) {
            var length = args.Length - startIndex;
            var copyIndex = 0;
            var remainingArgs = new string[length];
            for(int i = startIndex; i < args.Length; ++i) {
                remainingArgs[copyIndex] = args[i];
                ++copyIndex;
            }
            return remainingArgs;
        }

        private void InvokeGlobalEvent(string eventName) {
            GlobalEventHandler.InvokeEvent(eventName);
        }
        
        private void InvokeRelativeEvent(string eventName, int instanceId, object[] args) {
            RelativeEventHandler.InvokeEvent(eventName, cache[instanceId], args);
        }

        /// <summary>
        /// Executes a registered method within the event tables. If only one argument is parsed
        /// then the Global Event Table is executed.
        /// </summary>
        /// <param name="input">The user's input</param>
        public void TryExecuteCommand(string input) {
            // Trim the input of trailing white spaces and split the string into an array of strings.
            var args = input.Trim().Split(delimiter);
            
            if (args.Length > 1) {
                var parameters = argParser.ParseParameters(CopyArgs(args, 2));
                int intValue = argParser.TryParseInt(args[1]);

                if (cache.IsIdCached(intValue)) {
                    InvokeRelativeEvent(args[0], intValue, parameters);
                } else {
                    // Invoke a global event with an int parameter?
                    GlobalEventHandler.InvokeEvent(args[0], intValue);
                }
            } else {
                InvokeGlobalEvent(args[0]);
            }
        }
    }
}
