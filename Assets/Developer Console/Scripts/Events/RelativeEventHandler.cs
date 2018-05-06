using System;
using System.Reflection;
using System.Collections.Generic;

namespace GlobalEvents {

    using Type = System.Type;

    public static class RelativeEventHandler {

        private static IDictionary<object, IDictionary<string, Delegate>> relativeEventTable = new Dictionary<object, IDictionary<string, Delegate>>();

        private static Delegate GetDelegate(string key, IDictionary<string, Delegate> table) {
            var d = default(Delegate);
            table.TryGetValue(key, out d);
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

        private static void SubscribeEvent(object key, string eventName, Delegate handler) {
            if (key == null) {
#if UNITY_EDITOR
                UnityEngine.Debug.LogError("Object cannot be null for event subscription!");
#endif
                return;
            }

            var eventTable = default(IDictionary<string, Delegate>);
            if (relativeEventTable.TryGetValue(eventName, out eventTable)) {
                SubscribeEvent(eventTable, eventName, handler);
            } else {
                var entry = new Dictionary<string, Delegate>();
                entry.Add(eventName, handler);
                relativeEventTable.Add(key, entry);
            }
        }

        private static void UnsubscribeEvent(IDictionary<string, Delegate> eventTable, string eventName, Delegate handler) {
            var d = default(Delegate);
            if (eventTable.TryGetValue(eventName, out d)) {
                eventTable[eventName] = Delegate.Remove(d, handler);
            }
        }

        private static void UnsubscribeEvent(object key, string eventName, Delegate handler) {
            var eventTable = default(IDictionary<string, Delegate>);
            if (relativeEventTable.TryGetValue(eventName, out eventTable)) {
                UnsubscribeEvent(eventTable, eventName, handler);
            }
        }
        
        public static void InvokeEvent(object key, string eventName) {
            var relativeEvent = default(IDictionary<string, Delegate>);
            if (relativeEventTable.TryGetValue(key, out relativeEvent)) {
                var action = GetDelegate(eventName, relativeEvent) as Action;
                if (action != null) {
                    action();
                }
            }
        }
    }
}
