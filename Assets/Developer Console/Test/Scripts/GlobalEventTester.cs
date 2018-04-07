using DeveloperConsole.Utility;
using GlobalEvents;
using UnityEngine;

public class GlobalEventTester : MonoBehaviour {
    
    [SerializeField]
    private string eventName = "Output Handling";
    [SerializeField]
    private KeyCode executeEventKey = KeyCode.Z;

	private void Start () {
        GlobalEventHandler.SubscribeEvent<string, Color>(ConsoleEventConstants.AddOutputEventName, TestAddMessage);
	}

    private void Update() {
        if (Input.GetKeyUp(executeEventKey)) {
            GlobalEventHandler.InvokeEvent<string, Color>(ConsoleEventConstants.AddOutputEventName, "Hello", Color.red);
        }
    }
    
    private void DebugEvent () {
        Debug.Log("The event executed!");
    }

    private void TestAddMessage(string message, Color color) {
        Debug.LogWarningFormat("Message: {0}, Color: {1}", message, color);
    }
}
