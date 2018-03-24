using System.Text.RegularExpressions;

namespace GlobalEvents {

    using DeveloperConsole.Utility;
    using Type = System.Type;

    /// <summary>
    /// A class which handles parsing arguments to ensure validity of the arguments passed.
    /// </summary>
    internal class ArgParser {

        internal readonly Regex eventNameRegex;
        internal readonly Regex intRegex;
        internal readonly Regex floatRegex;
        internal readonly Regex stringRegex;

        internal ArgParser() {
            eventNameRegex  = new Regex(@"\s");
            intRegex        = new Regex(@"^\d$");
            floatRegex      = new Regex(@"^[0-9]*(?:\.[0-9]*(f)*?)?$");
            stringRegex     = new Regex(@"^.+");
        }

        internal ArgParser(string eventPattern, string intPattern, string floatPattern, string stringPattern) {
            eventNameRegex  = new Regex(@eventPattern);
            intRegex        = new Regex(@intPattern);
            floatRegex      = new Regex(@floatPattern);
            stringRegex     = new Regex(@stringPattern);
        }

        private Tuple<Type, object> GetArgumentTypeValue(string arg) {
            if (intRegex.IsMatch(arg)) {
                return Tuple<Type, object>.Create(typeof(int), arg);
            } else if (floatRegex.IsMatch(arg)) {
                return Tuple<Type, object>.Create(typeof(float), arg);
            } else if (stringRegex.IsMatch(arg)) {
                return Tuple<Type, object>.Create(typeof(string), arg);
            }
            return Tuple<Type, object>.Create(typeof(void), null);
        }

        /// <summary>
        /// Checks if the argument is considered an integer type.
        /// </summary>
        private bool IsArgInt(string arg) {
            return intRegex.IsMatch(arg);
        }
        
        /// <summary>
        /// Checks if the argument is considered a valid float value.
        /// </summary>
        private bool IsArgFloat(string arg) {
            return floatRegex.IsMatch(arg);
        }
        
        /// <summary>
        /// Checks if the argument is considered a valid float value≥
        /// </summary>
        private bool IsArgString(string arg) {
            return stringRegex.IsMatch(arg);
        }

        /// <summary>
        /// Checks if the eventName is valid.
        /// </summary>
        /// <returns>True, if there is no space within the eventName".</returns>
        internal bool IsEventNameValid(string eventName) {
            return !eventNameRegex.IsMatch(eventName);
        }
        
        /// <summary>
        /// Builds the invocation call arguments for the invokeEvent<T...> call.
        /// </summary>
        internal Tuple<Type, object>[] BuildInvokationArgs(string[] args) {
            var argLength = args.Length;
            var arguments = new Tuple<Type, object>[argLength];

            for(int i = 0; i < argLength; i++) {
                arguments[i] = GetArgumentTypeValue(args[i]);
            }

            return arguments;
        }

        internal T GetArgument<T>(object arg) {
            return (T) System.Convert.ChangeType(arg, typeof(T));
        }
    }
}
