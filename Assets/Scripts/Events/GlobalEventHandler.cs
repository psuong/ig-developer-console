using System;
using System.Collections.Generic;
using UnityEngine;

namespace GlobalEvents {

	/// <summary>
	/// Stores global events to be freely accessed on a global level.
	/// </summary>
	public class GlobalEventHandler {

		private static IDictionary<string, Action> globalEvents = new Dictionary<string, Action> ();
		// TODO: Generify the object relative events
		private static Dictionary<MonoBehaviour, Dictionary<string, Action>> monoRelativeEvents;
		private static Dictionary<ScriptableObject, Dictionary<string, Action>> scriptableRelativeEvents;

		private static Action TryGetAction (string name) {
			Action action;
			globalEvents.TryGetValue (name, out action);
			return action;
		}

		/// <summary>
		/// Subscribes an event to the global table.
		/// </summary>
		/// <param name="name">The ID of the event</param>
		/// <param name="action">The function to subscribe</param>
		public static void SubscribeGlobalEvent (string name, Action action) {
			if (GlobalEventHandler.globalEvents.ContainsKey (name)) {
				globalEvents[name] += action;
			} else {
				globalEvents.Add (name, action);
			}
		}

		/// <summary>
		/// Removes an event from the global table if it exists.
		/// </summary>
		/// <param name="name"></param>
		/// <param name="action"></param>
		public static void UnsubscribeGlobalEvent (string name, Action action) {
			if (GlobalEventHandler.globalEvents.ContainsKey (name)) {
				globalEvents[name] -= action;
			}
		}

		/// <summary>
		/// Invokes a global event given a valid event name.
		/// </summary>
		/// <param name="name">The ID of the event</param>
		public static void InvokeEvent (string name) {
			var action = TryGetAction (name);
			if (action != null) {
				action ();
			}
		}

	}
}