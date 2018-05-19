using Console.UI;
using GlobalEvents;
using UnityEngine;

namespace Console.Demo {

    public class SampleHealth : MonoBehaviour {
        
        [SerializeField, Tooltip("What is the max health of the agent?")]
        private float maxHealth = 100f;

        private float health;

        private void Start() {
            health = maxHealth;
        }

        private void OnEnable() {
            // Subscribe all overloaded methods with their respective events
            /*
            RelativeEventHandler.SubscribeEvent("Heal", this, "Heal");
            RelativeEventHandler.SubscribeEvent("Damage", this, "Damage");
            */

            // Cache the instance Id
            IdCache.CacheInstanceId(GetInstanceID(), this);
        }

        private void OnDisable() {
            // Unsubscribe all overloaded methods with their respective events
            /*
            RelativeEventHandler.UnsubscribeEvent("Heal", this, "Heal");
            RelativeEventHandler.UnsubscribeEvent("Damage", this, "Damage");
            */

            // Remove the instance Id
            IdCache.RemoveInstanceId(GetInstanceID());
        }

        private void Heal(float amount) {
            health += amount;
            ConsoleOutput.Log(string.Format("{0} healed {1} health", name, amount), Color.green);
        }

        private void Heal(float amount, float multiplier) {
            health += (amount * multiplier);
            ConsoleOutput.Log(string.Format("{0} healed {1} health with a multpilier of {2}", name, amount, multiplier), Color.green);
        }

        private void Damage(float amount) {
            health = Mathf.Clamp(0, maxHealth, health - amount);
            ConsoleOutput.Log(string.Format("{0} took {1} damage", name, amount), Color.blue);
        }

        private void Damage(float amount, float multiplier) {
            health = Mathf.Clamp(0, maxHealth, health - amount * multiplier);
            ConsoleOutput.Log(string.Format("{0} took {1} damage", name, amount * multiplier), Color.blue);
        }
    }
}
