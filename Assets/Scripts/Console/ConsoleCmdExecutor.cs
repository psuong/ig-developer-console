using UnityEngine;
using UnityEngine.UI;

namespace DeveloperConsole {
	using GlobalEvents;

	public class ConsoleCmdExecutor : MonoBehaviour {

		/// <summary>
		/// Executes a delegate within the Global Event Table if it exists.
		/// </summary>
		/// <param name="input">The user's input</param>
		public void ExecuteCommand (string input) {
			if (GlobalEventHandler.IsKeyValid (input)) {
				// TODO: Log that the event executed in the console.
				GlobalEventHandler.InvokeEvent (input);
			} else {
				// TODO: Log that the input is not in the global event table.
			}
		}

	}
}