using System;
using System.Collections.Generic;
using System.Reflection;

namespace GlobalEvents {

    using Object = UnityEngine.Object;

    public static class RelativeEventHandler {

        private static IDictionary<string, object> relativeEventTable = new Dictionary<string, object>();
        
        private static object GetRelativeObject(string methodName) {
            var obj = default(object);
            relativeEventTable.TryGetValue(methodName, out obj);
            return obj;
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

        private static void SubscribeEvent(string methodName, object instance) {
            relativeEventTable.Add(methodName, instance);
        }

        private static void UnsubscribeEvent(string methodName) {
            relativeEventTable.Remove(methodName);
        }

        public static void InvokeEvent(string methodName, params object[] args) {
            var instance = GetRelativeObject(methodName);
            if (instance != null) {
                var type = instance.GetType();
                InvokeEvent(type, instance, methodName, args);
            }
        }
    }
}
