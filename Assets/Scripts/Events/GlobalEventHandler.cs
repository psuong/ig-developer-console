using System;
using System.Collections.Generic;

namespace GlobalEvents {

    /// <summary>
    /// Stores global events to be freely accessed on a global level.
    /// </summary>
    public static class GlobalEventHandler {

        private static IDictionary<string, Delegate> globalEventTable = new Dictionary<string, Delegate>();
        private static IDictionary<object, IDictionary<string, Delegate>> relativeEventTable = new Dictionary<object, IDictionary<string, Delegate>>();

        private static Delegate GetDelegate(string eventName, IDictionary<string, Delegate> eventTable) {
            Delegate d;
            eventTable.TryGetValue(eventName, out d);
            return d;
        }

        private static IDictionary<string, Delegate> GetRelativeEventTable(object key) {
            IDictionary<string, Delegate> eventTable;
            relativeEventTable.TryGetValue(key, out eventTable);
            return eventTable;
        }

        private static void SubscribeEvent(string eventName, Delegate handler) {
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
        /// Invokes a registered event within the global event table.
        /// </summary>
        /// <param name="eventName">The identifier for the event.</param>
        public static void InvokeEvent(string eventName) {
            var action = GetDelegate(eventName, globalEventTable) as Action;
            if (action != null) {
                action();
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
        /// Is the event registered within the global event table?
        /// </summary>
        /// <param name="eventName">The identifier of the event.</param>
        /// <returns>True, if registered.</returns>
        public static bool IsEventSubscribed(string eventName) {
            return globalEventTable.ContainsKey(eventName);
        }
        
        /// <summary>
        /// Registers an event to the global event table.
        /// </summary>
        /// <param name="eventName">The identifier for the event to register.</param>
        /// <param name="action">The function to register.</param>
        public static void SubscribeEvent(string eventName, Action action) {
            SubscribeEvent(eventName, action as Delegate);
        }
        
        /// <summary>
        /// Registers an event to the global event table with two arguements.
        /// </summary>
        /// <param name="eventName">The identifier for the event to register.</param>
        /// <param name="action">The function to register with two arguments.</param>
        public static void SubscribeEvent<T1, T2>(String eventName, Action<T1, T2> action) {
            SubscribeEvent(eventName, action as Delegate);
        }
        
        /// <summary>
        /// Unregisters an event from the global event table.
        /// </summary>
        /// <param name="eventName">The identifier for the event to remove.</param>
        /// <param name="action">The function to remove.</param>
        public static void UnsubscribeEvent(string eventName, Action action) {
            UnsubscribeEvent(eventName, action as Delegate);
        }
        
        /// <summary>
        /// Unregisters an event from the global event table.
        /// </summary>
        /// <param name="eventName">The identifier for the event to remove.</param>
        /// <param name="action">The function to remove.</param>
        public static void UnsubscribeEvent<T1, T2>(string eventName, Action<T1, T2> action) {
            UnsubscribeEvent(eventName, action as Delegate);
        }
    }
}
