using System;
using System.Text.RegularExpressions;

namespace GlobalEvents {

    /// <summary>
    /// A class which handles parsing arguments to ensure validity of the arguments passed.
    /// </summary>
    internal class ArgParser {

        internal readonly Regex eventNameRegex;
        internal readonly Regex charRegex;
        internal readonly Regex intRegex;
        internal readonly Regex floatRegex;
        internal readonly Regex stringRegex;

        internal ArgParser() {
            eventNameRegex  = new Regex(@"\s");
            charRegex       = new Regex(@"^\s{1}$");
            intRegex        = new Regex(@"^\d$");
            floatRegex      = new Regex(@"^[0-9]*(?:\.[0-9]*(f)*?)?$");
            stringRegex     = new Regex(@"^.+");
        }

        internal ArgParser(string eventPattern, string charPattern, string intPattern, string floatPattern, string stringPattern) {
            eventNameRegex  = new Regex(@eventPattern);
            charRegex       = new Regex(@charPattern);
            intRegex        = new Regex(@intPattern);
            floatRegex      = new Regex(@floatPattern);
            stringRegex     = new Regex(@stringPattern);
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
        /// Checks if the eventName is valid.
        /// </summary>
        /// <returns>True, if there is no space within the eventName".</returns>
        internal bool IsEventNameValid(string eventName) {
            return !eventNameRegex.IsMatch(eventName);
        }

        internal dynamic GetArgValue(String value) {
            if (IsArgInt(value)) {
                return int.Parse(value);
            } else if (IsArgFloat(value)) {
                return float.Parse(value);
            } else if (IsArgChar(value)) {
                return value[0];
            }
            return value;
        }
    }
}
