using System.Collections.Generic;

namespace GlobalEvents {

    using Object = UnityEngine.Object;

    public static class RelativeEventHandler {

        private static IDictionary<string, IDictionary<object, IList<string>>> relativeEventTable = new Dictionary<string, IDictionary<object, IList<string>>>();

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

        private static void SubscribeEvent(string eventName, object instance, string method) {
            var eventTable = default(IDictionary<object, IList<string>>);
            if (relativeEventTable.TryGetValue(eventName, out eventTable)) {
                AddRelativeEvent(instance, method, ref eventTable);
            } else {
                IDictionary<object, IList<string>> relativeEvent = new Dictionary<object, IList<string>>();
                AddRelativeEvent(instance, method, ref relativeEvent);
                relativeEventTable.Add(eventName, relativeEvent);
            }
        }
    }
}
