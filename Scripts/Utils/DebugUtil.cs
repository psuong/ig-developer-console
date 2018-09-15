using UDebug = UnityEngine.Debug;

namespace Console {

    /// <summary>
    /// A utility class to short hand UnityEngine.Debug.
    /// </summary>
    public static class DebugUtil {

        /// <summary>
        /// Utility function to log a message to the Unity's console.
        /// </summary>
        /// <param name="message">The message to output.</param>
        public static void Log(string message) {
            UDebug.Log(message);
        }

        /// <summary>
        /// Utility function to log a message with a format of the string to Unity's console.
        /// </summary>
        /// <param name="format">The format of the message.</param>
        /// <param name="argv">The optional arguments to pass to the format.</param>
        public static void LogFormat(string format, params string[] argv) {
            UDebug.LogFormat(format, argv);
        }

        /// <summary>
        /// Utility function to log an error to Unity's console.
        /// </summary>
        /// <param name="message">The error message to output.</param>
        public static void LogError(string message) {
            UDebug.LogError(message);
        }

        /// <summary>
        /// Utility function to log an error with a format of the string to Unity's console.
        /// </summary>
        /// <param name="format">The format of the error message.</param>
        /// <param name="argv">A list of optional arguments.</param>
        public static void LogErrorFormat(string format, params string[] argv) {
            UDebug.LogErrorFormat(format, argv);
        }

        /// <summary>
        /// Utility function to log a warning to Unity's console.
        /// </summary>
        /// <param name="message">The warning message to output.</param>
        public static void LogWarning(string message) {
            UDebug.LogWarning(message);
        }

        /// <summary>
        /// Utility function to log an error with a format of the string to Unity's console.
        /// </summary>
        /// <param name="format">The format of the error message.</param>
        /// <param name="argv">A list of optional arguments.</param>
        public static void LogWarningFormat(string format, params string[] argv) {
            UDebug.LogWarningFormat(format, argv);
        }
    }
}
