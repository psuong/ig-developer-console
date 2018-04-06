using UnityEngine;

namespace DeveloperConsole {

    using GlobalEvents;

    public class ConsoleCmdExecutor : MonoBehaviour {
        
        [SerializeField, Tooltip("What is the separator of the command line input? By default it is a space character.")]
        private char delimiter = ' ';

        [SerializeField, Tooltip("Should the regex be user defined?")]
        private bool useCustomRegexPatterns;

        [SerializeField]
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

        private void InvokeRelativeEvent(string eventName, object[] args) {
            RelativeEventHandler.InvokeEvent(eventName, args, argParser.GetParameterTypes(args));
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
                var parameters = argParser.ParseParameters(CopyArgs(args, 1));
                InvokeRelativeEvent(args[0], parameters);
            } else {
                InvokeGlobalEvent(args[0]);
            }
        }
    }
}
