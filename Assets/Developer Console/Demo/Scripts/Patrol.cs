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
            
            MoveToNextPoint();
        }

        private void Update() {
            if (IsNearDestination()) {
                MoveToNextPoint();
            }
        }

        private void MoveToNextPoint() {
            index = (index + 1) % patrolPoints.Length;
            var position = patrolPoints[index].position;

            agent.SetDestination(position);
        }

        private bool IsNearDestination() {
            return Vector3.Distance(transform.position, agent.destination) <= stoppingDistance;
        }
    }
}
