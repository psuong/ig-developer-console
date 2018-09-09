using Console.UI;
using Console;
using System.Collections.Generic;
using UnityEngine;

namespace Console {
    [CreateAssetMenu(fileName = "Id Cache", menuName = "Developer Console/Id Cache")]   
    public class IdCache : ScriptableObject {
        
        [SerializeField, Tooltip("What color should the message be for displaying ids?")]
        private Color messageColor = Color.green;

        private IDictionary<int, object> cache;

        private void OnEnable() {
            cache = new Dictionary<int, object>();
            GlobalEventHandler.SubscribeEvent<int, object>(Events.ConsoleEvents.CacheEvent, CacheId);
            GlobalEventHandler.SubscribeEvent<int>(Events.ConsoleEvents.RemoveEvent, RemoveId);
            GlobalEventHandler.SubscribeEvent<int>(Events.ConsoleEvents.ShowIdEvent, DisplayCacheIds);
        }

        private void OnDisable() {
            cache.Clear();
            GlobalEventHandler.UnsubscribeEvent<int, object>(Events.ConsoleEvents.CacheEvent, CacheId);
            GlobalEventHandler.UnsubscribeEvent<int>(Events.ConsoleEvents.CacheEvent, RemoveId);
            GlobalEventHandler.UnsubscribeEvent<int>(Events.ConsoleEvents.ShowIdEvent, DisplayCacheIds);
        }
        
        private void CacheId(int id, object instance) {
            if (!cache.ContainsKey(id)) {
                cache.Add(id, instance);
            } else {
                cache[id] = instance;
            }
        }

        private void RemoveId(int id) {
            cache.Remove(id);
        }

        private void DisplayCacheIds(int size) {
            int index = 0;
            string message = "\n";
            foreach(var entry in cache) {
                message += string.Format("{0} | {1}\n", entry.Key, entry.Value);
                index++;
                if (index > size) {
                    break;
                }
            }
            ConsoleOutput.Log(message, messageColor);  
        }

        /// <summary>
        /// Invokes a global event which store the instance Id of an object and the associated object.
        /// This invokes all instances of the scriptable object of type IdCache.
        /// </summary>
        /// <param name="id">The unique id of the object.</param>
        /// <param name="instance">The object associated with the id.</param>
        public static void CacheInstanceId(int id, object instance) {
            GlobalEventHandler.InvokeEvent<int, object>(Events.ConsoleEvents.CacheEvent, id, instance);
        }
        
        /// <summary>
        /// Invokes a global event which removes the instance Id fo an object and its associated object.
        /// This invokes all instances of the scriptable object of type IdCache.
        /// </summary>
        /// <param name="id">The Id to remove.</param>
        public static void RemoveInstanceId(int id) {
            GlobalEventHandler.InvokeEvent<int>(Events.ConsoleEvents.RemoveEvent, id);
        }
        
        /// <summary>
        /// Is the Id stored?
        /// </summary>
        /// <returns>true, if the Id is stored.</returns>
        public bool IsIdCached(int id) {
            return cache.ContainsKey(id);
        }
        
        /// <summary>
        /// Returns the value of the key if it exists. Otherwise the default value of an object is returned.
        /// </summary>
        /// <returns>The value of the object associated with the key.</returns>
        public object this[int key] {
            get {
                var value = default(object);
                cache.TryGetValue(key, out value);
                return value;
            }
        }
    }
}
