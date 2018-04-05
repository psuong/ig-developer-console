using System;
using System.Collections.Generic;
using System.Reflection;

namespace GlobalEvents {

    using Object = UnityEngine.Object;

    public static class RelativeEventHandler {

        private static IDictionary<string, object> relativeEventTable = new Dictionary<string, object>();

        private static object GetRelativeObject(string methodName) {
            var instance = default(object);
            relativeEventTable.TryGetValue(methodName, out instance);
            return instance;
        }

        private static void InvokeEvent(Type type, object instance, string methodName, params object[] args) {
            try {
                MethodInfo methodInfo = type.GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public);
                methodInfo.Invoke(instance, args);
            } catch (Exception err) {
#if UNITY_EDITOR
                UnityEngine.Debug.LogError(err);
#endif
            }
        }
    
        /// <summary>
        /// Invokes a method associated with an object that is registered to the relativeEventTable. This uses Reflections and
        /// is for .NET 3.5.
        /// </summary>
        /// <param name="methodName">The method to subscribe.</param>
        /// <param name="args">The arugments used to invoke the method.</param>
        public static void InvokeEvent(string methodName, params object[] args) {
            var instance = GetRelativeObject(methodName);
            if (instance != null) {
                var type = instance.GetType();
                InvokeEvent(type, instance, methodName, args);
            }
        }
        
        /// <summary>
        /// Registers a method via its definition and the associated object to the relativeEventTable.
        /// </summary>
        /// <param name="methodName">The exact declaration of the method (e.g. void foo(), a string "foo" must be registered.</param>
        /// <param name="instance">The instance of the object to register.</param>
        public static void SubscribeEvent(string methodName, object instance) {
            if (instance == null) {
#if UNITY_EDITOR
                UnityEngine.Debug.LogError("Object instance to subscribe is null!");
#endif
                return;
            }

            var keyValuePair = new KeyValuePair<string, object>(methodName, instance);

            if (relativeEventTable.Contains(keyValuePair)) {
#if UNITY_EDITOR
                UnityEngine.Debug.LogErrorFormat("The event: {0} for {1} already exists within the RelativeEventHandler!", methodName, instance);
#endif
                return;
            }
            relativeEventTable.Add(methodName, instance);
        }
        
        /// <summary>
        /// The method to unregister from the relativeEventTable.
        /// </summary>
        public static void UnsubscribeEvent(string methodName) {
            relativeEventTable.Remove(methodName);
        }
    }
}
