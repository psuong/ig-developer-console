using System;
using System.Text.RegularExpressions;

namespace DeveloperConsole {

    public class ArgParser {

        internal readonly Regex eventNameRegex;
        internal readonly Regex charRegex;
        internal readonly Regex intRegex;
        internal readonly Regex floatRegex;
        internal readonly Regex stringRegex;

        public ArgParser() {
            eventNameRegex  = new Regex(@"\s");
            charRegex       = new Regex(@"^\s{1}$");
            intRegex        = new Regex(@"^\d$");
            floatRegex      = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");
            stringRegex     = new Regex(@"^.+");
        }

        public ArgParser(string eventPattern, string charPattern, string intPattern, string floatPattern, string stringPattern) {
            eventNameRegex  = new Regex(@eventPattern);
            charRegex       = new Regex(@charPattern);
            intRegex        = new Regex(@intPattern);
            floatRegex      = new Regex(@floatPattern);
            stringRegex     = new Regex(@stringPattern);
        }

        private object GetParameterValue(string arg) {
            if (IsArgInt(arg)) {
                return int.Parse(arg);
            } else if (IsArgFloat(arg)) {
                return float.Parse(arg);
            } else if (IsArgChar(arg)) {
                return char.Parse(arg);
            } else {
                return arg;
            }
        }

        private bool IsArgInt(string arg) {
            return intRegex.IsMatch(arg);
        }

        private bool IsArgFloat(string arg) {
            return floatRegex.IsMatch(arg);
        }

        private bool IsArgString(string arg) {
            return stringRegex.IsMatch(arg);
        }

        private bool IsArgChar(String arg) {
            return charRegex.IsMatch(arg);
        }

        /// <summary>
        /// Checks if the eventName has no spaces in between the value.
        /// </summary>
        /// <returns>True, if there is no space within the eventName".</returns>
        internal bool IsEventNameValid(string eventName) {
            return !eventNameRegex.IsMatch(eventName);
        }
        
        /// <summary>
        /// Parses all parameters into their value types.
        /// </summary>
        /// <returns>An array of objects with their value parsed counterparts.</returns>
        internal object[] ParseParameters(string[] args) {
            switch(args.Length) {
                case 1:
                    return new object[] { 
                        GetParameterValue(args[0])
                    };
                case 2:
                    return new object[] { 
                        GetParameterValue(args[0]),
                        GetParameterValue(args[1])
                    };
                case 3:
                    return new object[] {
                        GetParameterValue(args[0]),
                        GetParameterValue(args[1]),
                        GetParameterValue(args[2])
                    };
                case 4:
                    return new object[] {
                        GetParameterValue(args[0]),
                        GetParameterValue(args[1]),
                        GetParameterValue(args[2]),
                        GetParameterValue(args[3]),
                    };
                case 5:
                    return new object[] {
                        GetParameterValue(args[0]),
                        GetParameterValue(args[1]),
                        GetParameterValue(args[2]),
                        GetParameterValue(args[3]),
                        GetParameterValue(args[4])
                    };
                default:
                    return new object[] {};
            }
        }
    }
}
