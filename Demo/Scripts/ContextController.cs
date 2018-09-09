using Console;
using UnityEngine;
using System.Collections.Generic;

namespace Console.Demo {
    
    /**
     * Controls the context of each agent.
     */
    public class ContextController : MonoBehaviour {
        
        [SerializeField]
        private Patrol[] agents;
        
        private List<Patrol> activeAIs;
        private List<Patrol> inactiveAIs;

        private void Awake() {
            activeAIs = new List<Patrol>();
            inactiveAIs = new List<Patrol>();
        }

        private void OnEnable() {
            GlobalEventHandler.SubscribeEvent<object>("ToggleAI", SwapAgentState);
        }

        private void OnDisable() {
            GlobalEventHandler.UnsubscribeEvent<object>("ToggleAI", SwapAgentState);
        }
        
        private void Start() {
            // Stash the agents to an active list
            foreach(var agent in agents) {
                activeAIs.Add(agent);
            }
        }

        private void Update() {
            UpdateActiveAgents();
            UpdateInactiveAgents();
        }
        
        /**
         * Execute each update step for an active agent.
         */
        private void UpdateActiveAgents() {
            for (int i = 0; i < activeAIs.Count; i++) {
                activeAIs[i].UpdateAI();
            }
        }

        private void UpdateInactiveAgents() {
            for (int i = 0; i < inactiveAIs.Count; i++) {
                inactiveAIs[i].UpdateInactiveAI();
            }
        }
        
        /**
         * Utility method to swap the state of an agent from one list to another, think of this as changing 
         * an element in a column of a table.
         */
        private bool SwapAgentState(Patrol agent, List<Patrol> from, List<Patrol> to) {
            if (from.Remove(agent)) {
                to.Add(agent);
                return true;
            }
            return false;
        }
        
        /**
         * Just take the agent as a param and swap the state without knowing which state the agent is in.
         */
        private void SwapAgentState(object agent) {
            var arg = agent as Patrol;
            var isInactive = SwapAgentState(arg, activeAIs, inactiveAIs);
            if (isInactive && arg != null) {
                UI.ConsoleOutput.Log(string.Format("Swapped {0} to inactive", arg.name), Color.green);
            } else {
                SwapAgentState(arg, inactiveAIs, activeAIs);
                UI.ConsoleOutput.Log(string.Format("Swapped {0} to active", arg.name), Color.green);
            }
        }
    }
}
