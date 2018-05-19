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
            @"^(?i)(true|false)$",
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
                    customRegexPatterns[4],
                    customRegexPatterns[5]
                ) : new ArgParser();
            Assert.IsNotNull(cache, "No IdCache was found!");
        }

        /// <summary>
        /// Executes a registered method within the event tables. If only one argument is parsed
        /// then the Global Event Table is executed.
        /// </summary>
        /// <param name="input">The user's input</param>
        public void TryExecuteCommand(string input) {
            // Trim the input of trailing white spaces and split the string into an array of strings.
            var args = input.Trim().Split(delimiter);
            var length = args.Length;

            // TODO: Implement a safe invoke method for invoking both global & relative events based on the # of args are in the input.
        }
    }
}
