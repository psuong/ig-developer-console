using UnityEngine;
using UnityEngine.AI;

namespace Console.Demo {
    
    [RequireComponent(typeof(NavMeshAgent), typeof(AnimatorController))]
    public class Patrol : MonoBehaviour {
        
        [SerializeField]
        private Transform[] patrolPoints;
        [SerializeField]
        private float stoppingDistance = 1.5f;

        private NavMeshAgent agent;
        private AnimatorController controller;
        private int index = -1;
        
        private void Start() {
            agent = GetComponent<NavMeshAgent>();
            controller = GetComponent<AnimatorController>();
            
        }
        
        /**
         * Increment to the next patrol point.
         */
        private void MoveToNextPoint() {
            index = (index + 1) % patrolPoints.Length;
            var position = patrolPoints[index].position;

            agent.SetDestination(position);
        }
        
        /**
         * Checks if the distance is linearly close enough to move to the next point.
         */
        private bool IsNearDestination() {
            return Vector3.Distance(transform.position, agent.destination) <= stoppingDistance;
        }
        
        /// <summary>
        /// So I'm trying to eventually switch over examples to use the ECS/Data Oriented Styled system and perform things as an
        /// operation amongst a series instead of individual operation that each entity has.
        ///
        /// Call this in the update loop.
        /// </summary>
        public void UpdateAI() {
            if (IsNearDestination()) {
                MoveToNextPoint();
            }
        }
    }
}
