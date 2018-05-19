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
            @"^[-][\d]*$",
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

        private void InvokeGlobalEvent(string eventName, string[] args) {
            switch(args.Length) {
                case 4:
                    GlobalEventHandler.InvokeEvent(eventName, 
                            argParser.GetParameterValue(args[0]), argParser.GetParameterValue(args[1]),
                            argParser.GetParameterValue(args[2]), argParser.GetParameterValue(args[3]));
                    return;
                case 3:
                    GlobalEventHandler.InvokeEvent(eventName, 
                            argParser.GetParameterValue(args[0]), argParser.GetParameterValue(args[1]),
                            argParser.GetParameterValue(args[2]));
                    return;
                case 2:
                    GlobalEventHandler.InvokeEvent(eventName, 
                            argParser.GetParameterValue(args[0]), argParser.GetParameterValue(args[1]));
                    return;
                case 1:
                    GlobalEventHandler.InvokeEvent(eventName, 
                            argParser.GetParameterValue(args[0]));
                    return;
                default:
                    return;
            }
        }

        private void InvokeRelativeEvent(string eventName, string[] args) {
            int id = argParser.GetParameterValue(args[0]);

            if (cache.IsIdCached(id)) {
                var instance = cache[id];
                switch(args.Length) {
                    case 5:
                        RelativeEventHandler.InvokeEvent(eventName, instance, 
                                argParser.GetParameterValue(args[1]), argParser.GetParameterValue(args[2]),
                                argParser.GetParameterValue(args[3]), argParser.GetParameterValue(args[4]));
                        return;
                    case 4:
                        RelativeEventHandler.InvokeEvent(eventName, instance, 
                                argParser.GetParameterValue(args[1]), argParser.GetParameterValue(args[2]),
                                argParser.GetParameterValue(args[3]));
                        return;
                    case 3:
                        RelativeEventHandler.InvokeEvent(eventName, instance, 
                                argParser.GetParameterValue(args[1]), argParser.GetParameterValue(args[2]));
                        return;
                    case 2:
                        RelativeEventHandler.InvokeEvent(eventName, instance, 
                                argParser.GetParameterValue(args[1]));
                        return;
                    case 1:
                        RelativeEventHandler.InvokeEvent(eventName, instance);
                        return;
                    default:
#if UNITY_EDITOR
                        Debug.LogErrorFormat("Cannot invoke event: {0}!", eventName);
#endif
                        return;
                }
            }
        }

        /// <summary>
        /// Executes a registered method within the event tables. If only one argument is parsed
        /// then the Global Event Table is executed.
        /// </summary>
        /// <param name="input">The user's input</param>
        public void TryExecuteCommand(string input) {
            // Trim the input of trailing white spaces and split the string into an array of strings.
            var args = input.Trim().Split(delimiter);
            var @event = args[0];

            if (args.Length > 1) {
                var @params = CopyArgs(args, 1);
                
                InvokeGlobalEvent(@event, @params);
                InvokeRelativeEvent(@event, @params);
            } else {
                GlobalEventHandler.InvokeEvent(@event);
            }
        }
    }
}
