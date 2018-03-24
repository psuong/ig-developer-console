using System;
using System.Collections.Generic;

namespace GlobalEvents {

    using Regex = System.Text.RegularExpressions.Regex;

    /// <summary>
    /// Stores global events to be freely accessed on a global level.
    /// </summary>
    public static class GlobalEventHandler {

        private static IDictionary<string, Delegate> globalEventTable = new Dictionary<string, Delegate>();
        private static ArgParser argParser = new ArgParser();

        private static Delegate GetDelegate(string eventName, IDictionary<string, Delegate> eventTable) {
            Delegate d;
            eventTable.TryGetValue(eventName, out d);
            return d;
        }

        private static void SubscribeEvent(string eventName, Delegate handler) {
            if (!argParser.IsEventNameValid(eventName)) {
#if UNITY_EDITOR
                UnityEngine.Debug.LogErrorFormat("{0} is not a valid eventName, there should not be any spaces!", eventName);
#endif
            }
            Delegate d;
            if (globalEventTable.TryGetValue(eventName, out d)) {
                globalEventTable[eventName] = Delegate.Combine(d, handler);
            } else {
                globalEventTable.Add(eventName, handler);
            }
        }

        private static void UnsubscribeEvent(string eventName, Delegate handler) {
            Delegate d;
            if (globalEventTable.TryGetValue(eventName, out d)) {
                globalEventTable[eventName] = Delegate.Remove(d, handler);
            }
        }
        
        /// <summary> 
        /// Invokes a function registered event within the global event table.
        /// </summary>
        /// <param name="eventName">The identifier for the event.</param>
        public static void InvokeEvent(string eventName) {
            var action = GetDelegate(eventName, globalEventTable) as Action;
            if (action != null) {
                action();
            }
        }
        
        /// <summary>
        /// </summary>
        /// <param name="eventName">The identifier for the event.</param>
        /// <param name="arg1">The first argument to invoke the global event.</param>
        public static void InvokeEvent<T1>(string eventName, T1 arg1) {
            var action = GetDelegate(eventName, globalEventTable) as Action<T1>;
            if (action != null) {
                action(arg1);
            }
        }

        /// <summary> 
        /// Invokes a registered event within the global event table using two arguments.
        /// </summary>
        /// <param name="eventName">The identifier for the event.</param>
        /// <param name="arg1">The first argument to invoke the global event.</param>
        /// <param name="arg2">The second argument to invoke the global event.</param>
        public static void InvokeEvent<T1, T2>(string eventName, T1 arg1, T2 arg2) {
            var action = GetDelegate(eventName, globalEventTable) as Action<T1, T2>;
            if (action != null) {
                action(arg1, arg2);
            }
        }

        /// <summary> 
        /// Invokes a registered event within the global event table using three arguments.
        /// </summary>
        /// <param name="eventName">The identifier for the event.</param>
        /// <param name="arg1">The first argument to invoke the global event.</param>
        /// <param name="arg2">The second argument to invoke the global event.</param>
        /// <param name="arg3">The third argument to invoke the global event.</param>
        public static void InvokeEvent<T1, T2, T3>(string eventName, T1 arg1, T2 arg2, T3 arg3) {
            var action = GetDelegate(eventName, globalEventTable) as Action<T1, T2, T3>;
            if (action != null) {
                action(arg1, arg2, arg3);
            }
        }

        /// <summary> 
        /// Invokes a registered event within the global event table using four arguments.
        /// </summary>
        /// <param name="eventName">The identifier for the event.</param>
        /// <param name="arg1">The first argument to invoke the global event.</param>
        /// <param name="arg2">The second argument to invoke the global event.</param>
        /// <param name="arg3">The third argument to invoke the global event.</param>
        /// <param name="arg4">The fourth argument to invoke the glboal event.</param>
        public static void InvokeEvent<T1, T2, T3, T4>(string eventName, T1 arg1, T2 arg2, T3 arg3, T4 arg4) {
            var action = GetDelegate(eventName, globalEventTable) as Action<T1, T2, T3, T4>;
            if (action != null) {
                action(arg1, arg2, arg3, arg4);
            }
        }        
        /// <summary>
        /// Is there an event registered to the event table?
        /// </summary>
        /// <param name="eventName">The identifier of the event.</param>
        /// <returns>True, if registered.</returns>
        public static bool IsEventSubscribed(string eventName) {
            return globalEventTable.ContainsKey(eventName);
        }
        
        /// <summary>
        /// Registers a function to the global event table.
        /// </summary>
        /// <param name="eventName">The identifier for the event to register.</param>
        /// <param name="action">The function to register.</param>
        public static void SubscribeEvent(string eventName, Action action) {
            SubscribeEvent(eventName, action as Delegate);
        }
        
        /// <summary>
        /// Registers a function to the global event table with two arguements.
        /// </summary>
        /// <param name="eventName">The identifier for the event to register.</param>
        public static void SubscribeEvent<T1>(string eventName, Action<T1> action) {
            SubscribeEvent(eventName, action as Delegate);
        }
        
        /// <summary>
        /// Registers a function to the global event table with two arguements.
        /// </summary>
        /// <param name="eventName">The identifier for the event to register.</param>
        /// <param name="action">The function to register with two arguments.</param>
        public static void SubscribeEvent<T1, T2>(String eventName, Action<T1, T2> action) {
            SubscribeEvent(eventName, action as Delegate);
        }
        
        /// <summary>
        /// Registers a function to the global event table with three arguments.
        /// </summary>
        /// <param name="eventName">The identifier for the event to register.</param>
        /// <param name="action">The function to register with three arguments.</param>
        public static void SubscribeEvent<T1, T2, T3>(String eventName, Action<T1, T2, T3> action) {
            SubscribeEvent(eventName, action as Delegate);
        }
        
        /// <summary>
        /// Registers a function to the global event table with four arguments.
        /// </summary>
        /// <param name="eventName">The identifier for the event to register.</param>
        /// <param name="action">The function to register with four arguments.</param>
        public static void SubscribeEvent<T1, T2, T3, T4>(String eventName, Action<T1, T2, T3, T4> action) {
            SubscribeEvent(eventName, action as Delegate);
        }

        /// <summary>
        /// Unregisters a function from the global event table.
        /// </summary>
        /// <param name="eventName">The identifier for the event to remove.</param>
        /// <param name="action">The function to remove.</param>
        public static void UnsubscribeEvent(string eventName, Action action) {
            UnsubscribeEvent(eventName, action as Delegate);
        }

        /// <summary>
        /// Unregisters a function with one argument from the global event table.
        /// </summary>
        /// <param name="eventName">The identifier for the event to remove.</param>
        /// <param name="action">The function to remove.</param>
        public static void UnsubscribeEvent<T1>(string eventName, Action<T1> action) {
            UnsubscribeEvent(eventName, action as Delegate);
        }

        /// <summary>
        /// Unregisters a function with two arguments from the global event table.
        /// </summary>
        /// <param name="eventName">The identifier for the event to remove.</param>
        /// <param name="action">The function to remove.</param>
        public static void UnsubscribeEvent<T1, T2>(string eventName, Action<T1, T2> action) {
            UnsubscribeEvent(eventName, action as Delegate);
        }

        /// <summary>
        /// Unregisters a function with three arguments from the global event table.
        /// </summary>
        /// <param name="eventName">The identifier for the event to remove.</param>
        /// <param name="action">The function to remove.</param>
        public static void UnsubscribeEvent<T1, T2, T3>(string eventName, Action<T1, T2, T3> action) {
            UnsubscribeEvent(eventName, action as Delegate);
        }

        /// <summary>
        /// Unregisters a function with four arguments from the global event table.
        /// </summary>
        /// <param name="eventName">The identifier for the event to remove.</param>
        /// <param name="action">The function to remove.</param>
        public static void UnsubscribeEvent<T1, T2, T3, T4>(string eventName, Action<T1, T2, T3, T4> action) {
            UnsubscribeEvent(eventName, action as Delegate);
        }
    }
}
