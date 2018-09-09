using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace Console {

    /// <summary>
    /// Stores global events to be freely accessed on a global level.
    /// </summary>
    public static class GlobalEventHandler {


        private static IDictionary<string, Delegate> globalEventTable = new Dictionary<string, Delegate>();

        private static Delegate GetDelegate(string eventName) {
            Delegate d;
            globalEventTable.TryGetValue(eventName, out d);
            return d;
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
                globalEventTable[eventName] = Delegate.RemoveAll(d, handler);
            }
        }

        /// <summary>
        /// Returns a read only version of the global event table.
        /// </summary>
        public static IReadOnlyDictionary<string, Delegate> GlobalEventTable {
            get { return new ReadOnlyDictionary<string, Delegate>(globalEventTable); }
        }

        /// <summary> 
        /// Invokes a method registered event within the global event table.
        /// </summary>
        /// <param name="eventName">The identifier for the event.</param>
        public static void InvokeEvent(string eventName) {
            (GetDelegate(eventName) as Action)?.Invoke();
        }

        /// <summary>
        /// </summary>
        /// <param name="eventName">The identifier for the event.</param>
        /// <param name="arg1">The first argument to invoke the global event.</param>
        public static void InvokeEvent<T1>(string eventName, T1 arg1) {
            (GetDelegate(eventName) as Action<T1>)?.Invoke(arg1);
        }

        /// <summary> 
        /// Invokes a registered event within the global event table using two arguments.
        /// </summary>
        /// <param name="eventName">The identifier for the event.</param>
        /// <param name="arg1">The first argument to invoke the global event.</param>
        /// <param name="arg2">The second argument to invoke the global event.</param>
        public static void InvokeEvent<T1, T2>(string eventName, T1 arg1, T2 arg2) {
            (GetDelegate(eventName) as Action<T1, T2>)?.Invoke(arg1, arg2);
        }

        /// <summary> 
        /// Invokes a registered event within the global event table using three arguments.
        /// </summary>
        /// <param name="eventName">The identifier for the event.</param>
        /// <param name="arg1">The first argument to invoke the global event.</param>
        /// <param name="arg2">The second argument to invoke the global event.</param>
        /// <param name="arg3">The third argument to invoke the global event.</param>
        public static void InvokeEvent<T1, T2, T3>(string eventName, T1 arg1, T2 arg2, T3 arg3) {
            (GetDelegate(eventName) as Action<T1, T2, T3>)?.Invoke(arg1, arg2, arg3);
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
            (GetDelegate(eventName) as Action<T1, T2, T3, T4>)?.Invoke(arg1, arg2, arg3, arg4);
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
        /// Registers a method to the global event table.
        /// </summary>
        /// <param name="eventName">The identifier for the event to register.</param>
        /// <param name="action">The method to register.</param>
        public static void SubscribeEvent(string eventName, Action action) {
            SubscribeEvent(eventName, action as Delegate);
        }

        /// <summary>
        /// Registers a method to the global event table with two arguements.
        /// </summary>
        /// <param name="eventName">The identifier for the event to register.</param>
        /// <param name="action">The method to register with one argument.</param>
        public static void SubscribeEvent<T1>(string eventName, Action<T1> action) {
            SubscribeEvent(eventName, action as Delegate);
        }

        /// <summary>
        /// Registers a method to the global event table with two arguements.
        /// </summary>
        /// <param name="eventName">The identifier for the event to register.</param>
        /// <param name="action">The method to register with two arguments.</param>
        public static void SubscribeEvent<T1, T2>(String eventName, Action<T1, T2> action) {
            SubscribeEvent(eventName, action as Delegate);
        }

        /// <summary>
        /// Registers a method to the global event table with three arguments.
        /// </summary>
        /// <param name="eventName">The identifier for the event to register.</param>
        /// <param name="action">The method to register with three arguments.</param>
        public static void SubscribeEvent<T1, T2, T3>(String eventName, Action<T1, T2, T3> action) {
            SubscribeEvent(eventName, action as Delegate);
        }

        /// <summary>
        /// Registers a method to the global event table with four arguments.
        /// </summary>
        /// <param name="eventName">The identifier for the event to register.</param>
        /// <param name="action">The method to register with four arguments.</param>
        public static void SubscribeEvent<T1, T2, T3, T4>(String eventName, Action<T1, T2, T3, T4> action) {
            SubscribeEvent(eventName, action as Delegate);
        }

        /// <summary>
        /// Clears all the internal event table of all subscribed methods.
        /// </summary>
        public static void UnsubscribeAll() {
#if UNITY_EDITOR
            UnityEngine.Debug.LogFormat("<color=#808000ff>Removing {0} from the global event table.</color>", globalEventTable.Count);
#endif
            globalEventTable.Clear();
        }

        /// <summary>
        /// Unregisters a method from the global event table.
        /// </summary>
        /// <param name="eventName">The identifier for the event to remove.</param>
        /// <param name="action">The method to remove.</param>
        public static void UnsubscribeEvent(string eventName, Action action) {
            UnsubscribeEvent(eventName, action as Delegate);
        }

        /// <summary>
        /// Unregisters a method with one argument from the global event table.
        /// </summary>
        /// <param name="eventName">The identifier for the event to remove.</param>
        /// <param name="action">The method to remove.</param>
        public static void UnsubscribeEvent<T1>(string eventName, Action<T1> action) {
            UnsubscribeEvent(eventName, action as Delegate);
        }

        /// <summary>
        /// Unregisters a method with two arguments from the global event table.
        /// </summary>
        /// <param name="eventName">The identifier for the event to remove.</param>
        /// <param name="action">The method to remove.</param>
        public static void UnsubscribeEvent<T1, T2>(string eventName, Action<T1, T2> action) {
            UnsubscribeEvent(eventName, action as Delegate);
        }

        /// <summary>
        /// Unregisters a method with three arguments from the global event table.
        /// </summary>
        /// <param name="eventName">The identifier for the event to remove.</param>
        /// <param name="action">The method to remove.</param>
        public static void UnsubscribeEvent<T1, T2, T3>(string eventName, Action<T1, T2, T3> action) {
            UnsubscribeEvent(eventName, action as Delegate);
        }

        /// <summary>
        /// Unregisters a method with four arguments from the global event table.
        /// </summary>
        /// <param name="eventName">The identifier for the event to remove.</param>
        /// <param name="action">The method to remove.</param>
        public static void UnsubscribeEvent<T1, T2, T3, T4>(string eventName, Action<T1, T2, T3, T4> action) {
            UnsubscribeEvent(eventName, action as Delegate);
        }
    }
}
