using System.Text.RegularExpressions;

namespace GlobalEvents {

    /// <summary>
    /// A class which handles parsing arguments to ensure validity of the arguments passed.
    /// </summary>
    [System.Serializable]
    internal class ArgParser {

        internal readonly Regex eventNameRegex;
        internal readonly Regex intRegex;
        internal readonly Regex floatRegex;
        internal readonly Regex stringRegex;

        internal ArgParser() {
            eventNameRegex  = new Regex(@"\s");
            intRegex        = new Regex(@"^\d$");
            floatRegex      = new Regex(@"^[0-9]*(?:\.[0-9]*)?*?(f)$");
            stringRegex     = new Regex(@"^.+");
        }

        internal ArgParser(string eventPattern, string intPattern, string floatPattern, string stringPattern) {
            eventNameRegex  = new Regex(@eventPattern);
            intRegex        = new Regex(@intPattern);
            floatRegex      = new Regex(@floatPattern);
            stringRegex     = new Regex(@stringPattern);
        }

        /// <summary>
        /// Checks if the eventName is valid.
        /// </summary>
        /// <returns>True, if there is no space within the eventName".</returns>
        internal bool IsEventNameValid(string eventName) {
            return !eventNameRegex.IsMatch(eventName);
        }

        /// <summary>
        /// Checks if the argument is considered an integer type.
        /// </summary>
        internal bool IsArgInt(string arg) {
            return intRegex.IsMatch(arg);
        }
        
        /// <summary>
        /// Checks if the argument is considered a valid float value.
        /// </summary>
        internal bool IsArgFloat(string arg) {
            return floatRegex.IsMatch(arg);
        }
        
        /// <summary>
        /// Checks if the argument is considered a valid float value≥
        /// </summary>
        internal bool IsArgString(string arg) {
            return stringRegex.IsMatch(arg);
        }
    }
}
