using System;
using System.Collections.Generic;

namespace GlobalEvents {

    using Object = UnityEngine.Object;

    public static class RelativeEventHandler {

        private static IDictionary<Object, IDictionary<string, Delegate>> relativeEventTable = new Dictionary<Object, IDictionary<string, Delegate>>();

        private static Delegate GetDelegate(string eventName, IDictionary<string, Delegate> eventTable) {
            var d = default(Delegate);
            eventTable.TryGetValue(eventName, out d);
            return d;
        }

        private static void SubscribeEvent(IDictionary<string, Delegate> eventTable, string eventName, Delegate handler) {
            var d = default(Delegate);
            if (eventTable.TryGetValue(eventName, out d)) {
                eventTable[eventName] = Delegate.Combine(d, handler);
            } else {
                eventTable.Add(eventName, handler);
            }
        }

        private static void SubscribeEvent(Object obj, string eventName, Delegate handler) {
            if (obj == null) {
#if UNITY_EDITOR
                UnityEngine.Debug.LogError("Cannot subscribe an event to a null object!");
#endif
                return;
            }
            var eventTable = default(IDictionary<string, Delegate>);
            if (relativeEventTable.TryGetValue(obj, out eventTable)) {
                SubscribeEvent(eventTable, eventName, handler);
            } else {
                var entry = new Dictionary<string, Delegate>();
                entry.Add(eventName, handler);
                relativeEventTable.Add(obj, entry);
            }
        }

        private static void UnsubscribeEvent(IDictionary<string, Delegate> eventTable, string eventName, Delegate handler) {
            var d = default(Delegate);
            if (eventTable.TryGetValue(eventName, out d)) {
                eventTable[eventName] = Delegate.Remove(d, handler);
            }
        }

        private static void UnsubscribeEvent(Object obj, string eventName, Delegate handler) {
            var eventTable = default(IDictionary<string, Delegate>);
            if (relativeEventTable.TryGetValue(obj, out eventTable)) {
                UnsubscribeEvent(eventTable, eventName, handler);
            }
        }
        
        /// <summary>
        /// Registers a Unity Object and its associated event to the relative event table.
        /// </summary>
        /// <param name="obj">The object to subscribe.</param>
        /// <param name="eventName">The event's identifier to subscribe.</param>
        /// <param name="action">The function to register.</param>
        public static void SubscribeEvent(Object obj, string eventName, Action action) {
            SubscribeEvent(obj, eventName, action as Delegate);
        }
        
        /// <summary>
        /// Deregisters a Unity Object and its associated event from the relative event table.
        /// </summary>
        /// <param name="obj">the object to deregister.</param>
        /// <param name="eventName">The identifier for the event.</param>
        /// <param name="action">The function to remove.</param>
        public static void UnsubscribeEvent(Object obj, string eventName, Action action) {
            UnsubscribeEvent(obj, eventName, action as Delegate);
        }
    }
}
