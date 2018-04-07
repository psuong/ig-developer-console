using UnityEngine;
using GlobalEvents;
using DeveloperConsole.Utility;
using DeveloperConsole;
using System.Reflection;

public class Health : MonoBehaviour {

    private const string TGMEventName = "TGM";

    public float maxHealth = 100f;
    public bool isInvincible;

    private float currentHealth;

    private void OnEnable() {
        GlobalEventHandler.SubscribeEvent(TGMEventName, ToggleGodMode);
        // GlobalEventHandler.SubscribeEvent<float>("Heal", Heal);
        RelativeEventHandler.SubscribeEvent("Heal", this);
    }

    private void OnDisable() {
        GlobalEventHandler.UnsubscribeEvent(TGMEventName, ToggleGodMode);
        // GlobalEventHandler.UnsubscribeEvent<float>("Heal", Heal);
        RelativeEventHandler.UnsubscribeEvent("Heal");
    }

    private void Start() {
        currentHealth = maxHealth;
    }

    private void Die() {
        Debug.LogErrorFormat("{0} has died!", name);
    }

    private void ToggleGodMode() {
        // TODO: Add the output to the console.
        isInvincible = !isInvincible;

        GlobalEventHandler.InvokeEvent(
            ConsoleEventConstants.AddOutputEventName, 
            string.Format("{0} is in god mode? <color=#008000ff>{1}</color>", name, isInvincible),
            Color.red);

        Debug.LogFormat("{0} is in god mode? {1}", name, isInvincible);
    }

    public void Damage(float amount) {
        if (!isInvincible) {
            currentHealth -= amount;
            if (currentHealth <= 0f) {
                Die();
            }
        }
    }
    public void Heal(float amount) {
        currentHealth += amount;
        Debug.LogWarning(currentHealth);
        GlobalEventHandler.InvokeEvent("Heal", "Healed!", Color.green);
    }

    public void Heal(float amount, string message) {
        Debug.LogFormat("{0} {1}", amount, message);
    }
}
