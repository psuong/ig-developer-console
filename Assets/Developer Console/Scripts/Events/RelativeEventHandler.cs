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

        /// <summary>
        /// Invokes an event relative to an object.
        /// </summary>
        /// <param name="key">The object to invoke an event on.</param>
        /// <param name="eventName">The name of the event to invoke.</param>
        public static void InvokeEvent(object key, string eventName) {
            var relativeEvent = default(IDictionary<string, Delegate>);
            if (relativeEventTable.TryGetValue(key, out relativeEvent)) {
                var action = GetDelegate(eventName, relativeEvent) as Action;
                if (action != null) {
                    action();
                }
            }
        }

        /// <summary>
        /// Invokes an event relative to an object.
        /// </summary>
        /// <param name="key">The object to invoke an event on.</param>
        /// <param name="eventName">The name of the event to invoke.</param>
        /// <param name="arg1">The first argument needed to invoke the event.</param>
        public static void InvokeEvent<T1>(object key, string eventName, T1 arg1) {
            var relativeEvent = default(IDictionary<string, Delegate>);
            if (relativeEventTable.TryGetValue(key, out relativeEvent)) {
                var action = GetDelegate(eventName, relativeEvent) as Action<T1>;
                if (action != null) {
                    action(arg1);
                }
            }
        }

        /// <summary>
        /// Invokes an event relative to an object.
        /// </summary>
        /// <param name="key">The object to invoke an event on.</param>
        /// <param name="eventName">The name of the event to invoke.</param>
        /// <param name="arg1">The first argument needed to invoke the event.</param>
        /// <param name="arg2">The second argument needed to invoke the event.</param>
        public static void InvokeEvent<T1, T2>(object key, string eventName, T1 arg1, T2 arg2) {
            var relativeEvent = default(IDictionary<string, Delegate>);
            if (relativeEventTable.TryGetValue(key, out relativeEvent)) {
                var action = GetDelegate(eventName, relativeEvent) as Action<T1, T2>;
                if (action != null) {
                    action(arg1, arg2);
                }
            }
        }

        /// <summary>
        /// Invokes an event relative to an object.
        /// </summary>
        /// <param name="key">The object to invoke an event on.</param>
        /// <param name="eventName">The name of the event to invoke.</param>
        /// <param name="arg1">The first argument needed to invoke the event.</param>
        /// <param name="arg2">The second argument needed to invoke the event.</param>
        /// <param name="arg3">The third argument needed to invoke the event.<param>
        public static void InvokeEvent<T1, T2, T3>(object key, string eventName, T1 arg1, T2 arg2, T3 arg3) {
            var relativeEvent = default(IDictionary<string, Delegate>);
            if (relativeEventTable.TryGetValue(key, out relativeEvent)) {
                var action = GetDelegate(eventName, relativeEvent) as Action<T1, T2, T3>;
                if (action != null) {
                    action(arg1, arg2, arg3);
                }
            }
        }

        /// <summary>
        /// Invokes an event relative to an object.
        /// </summary>
        /// <param name="key">The object to invoke an event on.</param>
        /// <param name="eventName">The name of the event to invoke.</param>
        /// <param name="arg1">The first argument needed to invoke the event.</param>
        /// <param name="arg2">The second argument needed to invoke the event.</param>
        /// <param name="arg3">The third argument needed to invoke the event.</param>
        /// <param name="arg4">The fourth argument needed to invoke the event.</param>
        public static void InvokeEvent<T1, T2, T3, T4>(object key, string eventName, T1 arg1, T2 arg2, T3 arg3, T4 arg4) {
            var relativeEvent = default(IDictionary<string, Delegate>);
            if (relativeEventTable.TryGetValue(key, out relativeEvent)) {
                var action = GetDelegate(eventName, relativeEvent) as Action<T1, T2, T3, T4>;
                if (action != null) {
                    action(arg1, arg2, arg3, arg4);
                }
            }
        }

        /// <summary>
        /// Subscribes a method with no arguments to the event table.
        /// </summary>
        /// <param name="instance">The object instance to subscribe.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="action">The action to subscribe.</param>
        public static void SubscribeEvent(object instance, string eventName, Action action) {
            SubscribeEvent(instance, eventName, action as Delegate);
        }
        
        /// <summary>
        /// Subscribes a method with 1 argument to the event table.
        /// </summary>
        /// <param name="instance">The object instance to subscribe.</param>
        /// <param name="eventName">The name of the event to subscribe.</param>
        /// <param name="action">The action with 1 arg to subscribe.</param>
        public static void SubscribeEvent<T1>(object instance, string eventName, Action<T1> action) {
            SubscribeEvent(instance, eventName, action as Delegate);
        }

        /// <summary>
        /// Subscribes a method with 2 arguments to the event table.
        /// </summary>
        /// <param name="instance">The object instance to subscribe.</param>
        /// <param name="eventName">The name of the event to subscribe.</param>
        /// <param name="action">The action with 2 args to subscribe.</param>
        public static void SubscribeEvent<T1, T2>(object instance, string eventName, Action<T1, T2> action) {
            SubscribeEvent(instance, eventName, action as Delegate);
        }

        /// <summary>
        /// Subscribes a method with 3 arguments to the event table.
        /// </summary>
        /// <param name="instance">The object instance to subscribe.</param>
        /// <param name="eventName">The name of the event to subscribe.</param>
        /// <param name="action">The action with 3 args to subscribe.</param>
        public static void SubscribeEvent<T1, T2, T3>(object instance, string eventName, Action<T1, T2, T3> action) {
            SubscribeEvent(instance, eventName, action as Delegate);
        }

        /// <summary>
        /// Subscribes a method with 3 arguments to the event table.
        /// </summary>
        /// <param name="instance">The object instance to subscribe.</param>
        /// <param name="eventName">The name of the event to subscribe.</param>
        /// <param name="action">The action with 3 args to subscribe.</param>
        public static void SubscribeEvent<T1, T2, T3, T4>(object instance, string eventName, Action<T1, T2, T3, T4> action) {
            SubscribeEvent(instance, eventName, action as Delegate);
        }
        
        /// <summary>
        /// Removes a method from an event.
        /// </summary>
        /// <param name="instance">The object instance to remove.</param>
        /// <param name="eventName">The name of the event to unsubscribe from.</param>
        /// <param name="action">To method to remove.<param>
        public static void UnsubscribeEvent(object instance, string eventName, Action action) {
            UnsubscribeEvent(instance, eventName, action as Delegate);
        }

        /// <summary>
        /// Removes a method with 1 argument from an event.
        /// </summary>
        /// <param name="instance">The object instance to remove.</param>
        /// <param name="eventName">The name of the event to unsubscribe from.</param>
        /// <param name="action">To method to remove.<param>
        public static void UnsubscribeEvent<T1>(object instance, string eventName, Action<T1> action) {
            UnsubscribeEvent(instance, eventName, action as Delegate);
        }

        /// <summary>
        /// Removes a method with 2 arguments from an event.
        /// </summary>
        /// <param name="instance">The object instance to remove.</param>
        /// <param name="eventName">The name of the event to unsubscribe from.</param>
        /// <param name="action">To method to remove.<param>
        public static void UnsubscribeEvent<T1, T2>(object instance, string eventName, Action<T1, T2> action) {
            UnsubscribeEvent(instance, eventName, action as Delegate);
        }

        /// <summary>
        /// Removes a method with 3 arguments from an event.
        /// </summary>
        /// <param name="instance">The object instance to remove.</param>
        /// <param name="eventName">The name of the event to unsubscribe from.</param>
        /// <param name="action">To method to remove.<param>
        public static void UnsubscribeEvent<T1, T2, T3>(object instance, string eventName, Action<T1, T2, T3> action) {
            UnsubscribeEvent(instance, eventName, action as Delegate);
        }

        /// <summary>
        /// Removes a method with 4 arguments from an event.
        /// </summary>
        /// <param name="instance">The object instance to remove.</param>
        /// <param name="eventName">The name of the event to unsubscribe from.</param>
        /// <param name="action">To method to remove.<param>
        public static void UnsubscribeEvent<T1, T2, T3, T4>(object instance, string eventName, Action<T1, T2, T3, T4> action) {
            UnsubscribeEvent(instance, eventName, action as Delegate);
        }
    }
}
