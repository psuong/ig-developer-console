using System;
using System.Collections.Generic;

namespace GlobalEvents {

    public static class RelativeEventHandler {

        private static IDictionary<string, IDictionary<object, Delegate>> relativeEventTable = new Dictionary<string, IDictionary<object, Delegate>>();

        private static Delegate GetRelativeEvent(string eventName, object instance) {
            var eventTable = default(IDictionary<object, Delegate>);
            var d = default(Delegate);
            if (relativeEventTable.TryGetValue(eventName, out eventTable)) {
                if (eventTable.TryGetValue(instance, out d)) {
                    return d;
                }
            }
            return d;
        }

        private static void SubscribeRelativeEvent(object instance, Delegate handler, IDictionary<object, Delegate> eventTable) {
            var d = default(Delegate);
            if (eventTable.TryGetValue(instance, out d)) {
                eventTable[instance] = Delegate.Combine(d, handler);
            } else {
                eventTable.Add(instance, handler);
            }
        }

        private static void SubscribeEvent(string eventName, object instance, Delegate handler) {
            var eventTable = default(IDictionary<object, Delegate>);
            if (relativeEventTable.TryGetValue(eventName, out eventTable)) {
                SubscribeRelativeEvent(instance, handler, eventTable);
            } else {
                var entryTable = new Dictionary<object, Delegate>();
                SubscribeRelativeEvent(instance, handler, entryTable);
                relativeEventTable.Add(eventName, entryTable);
            }
        }

        private static void UnsubscribeRelativeEvent(object instance, Delegate handler, IDictionary<object, Delegate> eventTable) {
            var d = default(Delegate);
            if (eventTable.TryGetValue(instance, out d)) {
                eventTable[instance] = Delegate.Remove(d, handler);
            }
        }

        private static void UnsubscribeEvent(string eventName, object instance, Delegate handler) {
            var eventTable = default(IDictionary<object, Delegate>);
            if (relativeEventTable.TryGetValue(eventName, out eventTable)) {
                UnsubscribeRelativeEvent(instance, handler, eventTable);
            }
        }
            
        /// <summary>
        /// Invokes an event relative to an object with no arguments.
        /// </summary>
        /// <param name="eventName">The relative event to invoke.</param>
        /// <param name="instance">The object to invoke a relative event to.</param>
        public static void InvokeEvent(string eventName, object instance) {
            var action = GetRelativeEvent(eventName, instance) as Action;
            if (action != null) {
                action();
            }
        }

        /// <summary>
        /// Invokes an event relative to an object an argument.
        /// </summary>
        /// <param name="instance">The object to invoke a relative event to.</param>
        /// <param name="eventName">The relative event to invoke.</param>
        /// <param name="arg1">The parameter needed to invoke the event.</param>
        public static void InvokeEvent<T1>(string eventName, object instance, T1 arg1) {
            var action = GetRelativeEvent(eventName, instance) as Action<T1>;
            if (action != null) {
                action(arg1);
            }
        }

        /// <summary>
        /// Invokes an event relative to an object 2 arguments.
        /// </summary>
        /// <param name="instance">The object to invoke a relative event to.</param>
        /// <param name="eventName">The relative event to invoke.</param>
        /// <param name="arg1">The 1st parameter needed to invoke the event.</param>
        /// <param name="arg2">The 2nd parameter needed to invoke the event.</param>
        public static void InvokeEvent<T1, T2>(string eventName, object instance, T1 arg1, T2 arg2) {
            var action = GetRelativeEvent(eventName, instance) as Action<T1, T2>;
            if (action != null) {
                action(arg1, arg2);
            }
        }

        /// <summary>
        /// Invokes an event relative to an object 3 arguments.
        /// </summary>
        /// <param name="instance">The object to invoke a relative event to.</param>
        /// <param name="eventName">The relative event to invoke.</param>
        /// <param name="arg1">The 1st parameter needed to invoke the event.</param>
        /// <param name="arg2">The 2nd parameter needed to invoke the event.</param>
        /// <param name="arg3">The 3rd parameter needed to invoke the event.</param>
        public static void InvokeEvent<T1, T2, T3>(string eventName, object instance, T1 arg1, T2 arg2, T3 arg3) {
            var action = GetRelativeEvent(eventName, instance) as Action<T1, T2, T3>;
            if (action != null) {
                action(arg1, arg2, arg3);
            }
        }

        /// <summary>
        /// Invokes an event relative to an object 4 arguments.
        /// </summary>
        /// <param name="eventName">The relative event to invoke.</param>
        /// <param name="instance">The object to invoke a relative event to.</param>
        /// <param name="arg1">The 1st parameter needed to invoke the event.</param>
        /// <param name="arg2">The 2nd parameter needed to invoke the event.</param>
        /// <param name="arg3">The 3rd parameter needed to invoke the event.</param>
        /// <param name="arg4">The 4th parameter needed to invoke the event.</param>
        public static void InvokeEvent<T1, T2, T3, T4>(string eventName, object instance, T1 arg1, T2 arg2, T3 arg3, T4 arg4) {
            var action = GetRelativeEvent(eventName, instance) as Action<T1, T2, T3, T4>;
            if (action != null) {
                action(arg1, arg2, arg3, arg4);
            }
        }

        /// <summary>
        /// Subscribes a method with no arguments relative to an object.
        /// </summary>
        /// <param name="instance">The object instance to subscribe</param>
        /// <param name="eventName">The event identifier to subscribe to.</param>
        /// <param name="action">A method with no parameters to subscribe.</param>
        public static void SubscribeEvent(string eventName, object instance, Action action) {
            SubscribeEvent(eventName, instance, action as Delegate);
        }

        /// <summary>
        /// Subscribes a method with 1 required argument relative to an object.
        /// </summary>
        /// <param name="instance">The object instance to subscribe</param>
        /// <param name="eventName">The event identifier to subscribe to.</param>
        /// <param name="action">A method with 1 parameter to subscribe.</param>
        public static void SubscribeEvent<T1>(string eventName, object instance, Action<T1> action) {
            SubscribeEvent(eventName, instance, action as Delegate);
        }

        /// <summary>
        /// Subscribes a method with 2 required argument relative to an object.
        /// </summary>
        /// <param name="instance">The object instance to subscribe</param>
        /// <param name="eventName">The event identifier to subscribe to.</param>
        /// <param name="action">A method with 2 parameters to subscribe.</param>
        public static void SubscribeEvent<T1, T2>(string eventName, object instance, Action<T1, T2> action) {
            SubscribeEvent(eventName, instance, action as Delegate);
        }

        /// <summary>
        /// Subscribes a method with 3 required argument relative to an object.
        /// </summary>
        /// <param name="instance">The object instance to subscribe</param>
        /// <param name="eventName">The event identifier to subscribe to.</param>
        /// <param name="action">A method with 3 parameters to subscribe.</param>
        public static void SubscribeEvent<T1, T2, T3>(string eventName, object instance, Action<T1, T2, T3> action) {
            SubscribeEvent(eventName, instance, action as Delegate);
        }

        /// <summary>
        /// Subscribes a method with 4 required argument relative to an object.
        /// </summary>
        /// <param name="instance">The object instance to subscribe</param>
        /// <param name="eventName">The event identifier to subscribe to.</param>
        /// <param name="action">A method with 4 parameters to subscribe.</param>
        public static void SubscribeEvent<T1, T2, T3, T4>(string eventName, object instance, Action<T1, T2, T3, T4> action) {
            SubscribeEvent(eventName, instance, action as Delegate);
        }

        /// <summary>
        /// Removes a method from an event relative to an object.
        /// </summary>
        /// <param name="instance">The object instance to remove a relative event from.</param>
        /// <param name="eventName">The event identifier to remove a relative event from.</param>
        /// <param name="action">The method to remove the relative event from.</param>
        public static void UnsubscribeEvent(string eventName, object instance, Action action) {
            UnsubscribeEvent(eventName, instance, action as Delegate);
        }

        /// <summary>
        /// Removes a method with an argument from an event relative to an object.
        /// </summary>
        /// <param name="instance">The object instance to remove a relative event from.</param>
        /// <param name="eventName">The event identifier to remove a relative event from.</param>
        /// <param name="action">The method to remove the relative event from.</param>
        public static void UnsubscribeEvent<T1>(string eventName, object instance, Action<T1> action) {
            UnsubscribeEvent(eventName, instance, action as Delegate);
        }

        /// <summary>
        /// Removes a method with 2 arguments from an event relative to an object.
        /// </summary>
        /// <param name="instance">The object instance to remove a relative event from.</param>
        /// <param name="eventName">The event identifier to remove a relative event from.</param>
        /// <param name="action">The method to remove the relative event from.</param>
        public static void UnsubscribeEvent<T1, T2>(string eventName, object instance, Action<T1, T2> action) {
            UnsubscribeEvent(eventName, instance, action as Delegate);
        }

        /// <summary>
        /// Removes a method with 3 arguments from an event relative to an object.
        /// </summary>
        /// <param name="instance">The object instance to remove a relative event from.</param>
        /// <param name="eventName">The event identifier to remove a relative event from.</param>
        /// <param name="action">The method to remove the relative event from.</param>
        public static void UnsubscribeEvent<T1, T2, T3>(string eventName, object instance, Action<T1, T2, T3> action) {
            UnsubscribeEvent(eventName, instance, action as Delegate);
        }

        /// <summary>
        /// Removes a method with 4 arguments from an event relative to an object.
        /// </summary>
        /// <param name="instance">The object instance to remove a relative event from.</param>
        /// <param name="eventName">The event identifier to remove a relative event from.</param>
        /// <param name="action">The method to remove the relative event from.</param>
        public static void UnsubscribeEvent<T1, T2, T3, T4>(string eventName, object instance, Action<T1, T2, T3, T4> action) {
            UnsubscribeEvent(eventName, instance, action as Delegate);
        }
    }
}
