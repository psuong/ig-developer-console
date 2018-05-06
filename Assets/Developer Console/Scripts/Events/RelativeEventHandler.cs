using System;
using System.Reflection;
using System.Collections.Generic;

namespace GlobalEvents {

    using Type = System.Type;

    public static class RelativeEventHandler {

        private static IDictionary<string, IDictionary<object, Delegate>> relativeEventTable = new Dictionary<string, IDictionary<object, Delegate>>();

        /**

        private static bool TryAddMethod(string method, ref IList<string> subscribedMethods) {
            if (!subscribedMethods.Contains(method)) {
                subscribedMethods.Add(method);
                return true;
            } else {
#if UNITY_EDITOR
                UnityEngine.Debug.LogWarningFormat("{0} has already been subscribed to the relative event table.", method);
#endif
                return false;
            }
        }

        private static void AddRelativeEvent(object key, string method, ref IDictionary<object, IList<string>> relativeEvent) {
            var subscribedMethods = default(IList<string>);
            if (relativeEvent.TryGetValue(key, out subscribedMethods)) {
                TryAddMethod(method, ref subscribedMethods);
                relativeEvent[key] = subscribedMethods;
            } else {
                var methods = new List<string>();
                methods.Add(method);
                relativeEvent.Add(key, methods);
            }
        }

        private static IDictionary<object, IList<string>> GetRelativeEventTable(string eventName) {
            var eventTable = default(IDictionary<object, IList<string>>);
            relativeEventTable.TryGetValue(eventName, out eventTable);
            return eventTable;
        }

        private static void InvokeEvent(Type type, object instance, IList<string> methods, object[] args, Type[] typeArgs) {
            foreach(var method in methods) {
                try {
                    var methodInfo = type.GetMethod(
                            method,
                            BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance,
                            Type.DefaultBinder,
                            typeArgs,
                            null);
                    methodInfo.Invoke(instance, args);
                } catch (System.Exception err) {
#if UNITY_EDITOR
                    UnityEngine.Debug.LogError(err);
#endif
                }
            }
        }

        private static void InvokeEvent(string eventName, object instance, object[] args, Type[] typeArgs) {
            var eventTable = default(IDictionary<object, IList<string>>);
            if (relativeEventTable.TryGetValue(eventName, out eventTable)) {
                var subscribedMethods = default(IList<string>);

                if (eventTable.TryGetValue(instance, out subscribedMethods)) {
                    var instanceType = instance.GetType();
                    InvokeEvent(instanceType, instance, subscribedMethods, args, typeArgs);
                }
            }
        }
        
        /// <summary>
        /// Invokes methods associated with the event name given an object instance and the associated arguments.
        /// </summary>
        /// <param name="eventName">The name of the event to invoke.</param>
        /// <param name="instance">The instance of the object associated with the event.</param>
        /// <param name="args">An optional number of arguments used to invoke each event.</param>
        public static void InvokeEvent(string eventName, object instance, params object[] args) {
            if (instance == null) {
#if UNITY_EDITOR
                UnityEngine.Debug.LogError("Cannot invoke an event with a null instance!");
#endif
                return;
            }
            var types = Console.ArgParser.GetParameterTypes(args);
            InvokeEvent(eventName, instance, args, types);
        }
        
        /// <summary>
        /// Adds an event to the relative event table with the associated object instance and method.
        /// </summary>
        /// <param name="eventName">The name of event to register.</param>
        /// <param name="instance">The instance of the object associated with the event.</param>
        /// <param name="method">The name of the method to bind to the object.</param>
        public static void SubscribeEvent(string eventName, object instance, string method) {
            var eventTable = default(IDictionary<object, IList<string>>);
            if (relativeEventTable.TryGetValue(eventName, out eventTable)) {
                AddRelativeEvent(instance, method, ref eventTable);
            } else {
                IDictionary<object, IList<string>> relativeEvent = new Dictionary<object, IList<string>>();
                AddRelativeEvent(instance, method, ref relativeEvent);
                relativeEventTable.Add(eventName, relativeEvent);
            }
        }
        
        /// <summary>
        /// Removes an object instance and the name of the method registered to that instance.
        /// </summary>
        /// <param name="eventName">The name of the event to deregister.</param>
        /// <param name="instance">The instance of the object to deregister.</param>
        /// <param name="method">The string representation of the method to subscribe.</param>
        public static void UnsubscribeEvent(string eventName, object instance, string method) {
            var eventTable = default(IDictionary<object, IList<string>>);
            if (relativeEventTable.TryGetValue(eventName, out eventTable)) {
                var subscribedMethods = default(IList<string>);
                if (eventTable.TryGetValue(instance, out subscribedMethods)) {
                    subscribedMethods.Remove(method);
                    if (subscribedMethods.Count == 0) {
                        eventTable.Remove(instance);
                    }
                }
            }
        }
        **/
    }
}
