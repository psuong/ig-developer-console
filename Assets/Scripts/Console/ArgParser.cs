using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GlobalEvents {
    
    /// <summary>
    /// Represents the types feasible parameter arguments.
    /// </summary>
    internal enum ArgType {
        Default,
        Bool,
        Int,
        Float,
        Char,
        String
    }

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

        private bool GetBoolValue(string arg) {
            return (arg.ToLower() == "true");
        }

        private int GetIntValue(string arg) {
            return Int32.Parse(arg);
        }

        private float GetFloatValue(String arg) {
            return float.Parse(arg);
        }

        private String GetStringValue(String arg) {
            return arg;
        }

        private Char GetCharValue(String arg) {
            throw new NotImplementedException();
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

        /// <summary>
        /// Checks if the eventName is valid.
        /// </summary>
        /// <returns>True, if there is no space within the eventName".</returns>
        internal bool IsEventNameValid(string eventName) {
            return !eventNameRegex.IsMatch(eventName);
        }
        
        /// <summary>
        /// Returns an accompanying ArgType based on the arg input.
        /// </summary>
        /// <param name="arg">The input type of the argument.</param>
        /// <returns>ArgType</returns>
        internal ArgType GetArgType(String arg) {
            if (IsArgInt(arg)) {
                return ArgType.Int;
            } else if (IsArgFloat(arg)) {
                return ArgType.Float;
            } else if (IsArgString(arg)) {
                return ArgType.String;
            } 
            return ArgType.Default;
        }
    }
}
