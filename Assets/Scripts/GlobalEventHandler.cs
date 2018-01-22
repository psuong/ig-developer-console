using System;
using System.Collections.Generic;
using UnityEngine;

public class GlobalEventHandler {

	private static Dictionary<object, Dictionary<string, Delegate>> relativeEvents;
	private static Dictionary<string, Delegate> globalEvents;

	private static void RegisterEvent (string eventName, Delegate handler) {
		var d = default (Delegate);
		if (GlobalEventHandler.globalEvents.TryGetValue (eventName, out d)) {
			globalEvents[eventName] = Delegate.Combine (d, handler);
		} else {
			globalEvents.Add (eventName, handler);
		}
	}

	private static void RegisterEvent (object obj, string eventName, Delegate handler) {
		if (obj == null) {
			// Log an error here
		} else {
			var dictionary = default (Dictionary<string, Delegate>);
			if (!GlobalEventHandler.relativeEvents.TryGetValue (obj, out dictionary)) {
				dictionary = new Dictionary<string, Delegate> ();
				relativeEvents.Add (obj, dictionary);
			}
			var d = default (Delegate);
			if (dictionary.TryGetValue (eventName, out d)) {
				dictionary[eventName] = Delegate.Combine (d, handler);
			} else {
				dictionary.Add (eventName, handler);
			}
		}
	}

}