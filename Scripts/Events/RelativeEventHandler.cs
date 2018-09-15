using System;
using System.Collections.Generic;

namespace Console {

    /// <summary>
    /// The RelativeEventHandler stores registered functions specific to a particular object.
    /// </summary>
    public static class RelativeEventHandler {
        
        private static IDictionary<string, IDictionary<object, Delegate>> relativeEventTable = new Dictionary<string, IDictionary<object, Delegate>>();

#region Utilities
        // TODO: Combine the two utility functions together?
        private static IDictionary<object, Delegate> GetRelativeEventTable(string @event) {
            var table = default(IDictionary<object, Delegate>);
            relativeEventTable.TryGetValue(@event, out table);
            return table;
        }

        private static Delegate GetDelegate(object @object, ref IDictionary<object, Delegate> relativeEventTable) {
            if (@object == null) {
                throw new EventRegistrationException("A null object cannot be reigstered to a relative event table!");
            }

            var d = default(Delegate);
            relativeEventTable.TryGetValue(@object, out d);
            return d;
        }

        private static Delegate GetDelegate(string @event, object @object) {
            var table = GetRelativeEventTable(@event);
            return GetDelegate(@object, ref table);
        }
#endregion

        /// <summary>
        /// Subscribes a functionality to the relative event table. Functions only affect the particular object.
        /// </summary>
        /// <param name="@event">What is the event that the function is subscribing to?</param>
        /// <param name="@object">What is subscribing the functionality?</param>
        /// <param name="action">What function is being susbscribed?</param>
        public static void SubscribeEvent(string @event, object @object, Delegate action) {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <param name="@event">What is the event that a function is subscribing to?</param>
        /// <param name="@object">What is subscribing the said functionality?</param>
        /// <param name="@object">What function is being subscribed?</param>
        public static void SubscribeEvent<T1>(string @event, object @object, Action<T1> action) {
            SubscribeEvent(@event, @object, action as Delegate);
        }
    }
}
