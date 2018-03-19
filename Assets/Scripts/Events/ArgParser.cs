using System.Text.RegularExpressions;

namespace GlobalEvents {
    
    /// <summary>
    /// A class which handles parsing arguments to ensure validity of the arguments passed.
    /// </summary>
    internal class ArgParser {

        private Regex eventNameRegex;

        internal ArgParser() {
            eventNameRegex = new Regex(@"\s");
        }
        
        /// <summary>
        /// Checks if the eventName is valid.
        /// </summary>
        /// <returns>True, if there is no space within the eventName".</returns>
        internal bool IsEventNameValid(string eventName) {
            return !eventNameRegex.IsMatch(eventName);
        }
    }
}
