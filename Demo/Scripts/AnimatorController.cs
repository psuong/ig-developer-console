using UnityEngine;

namespace Console.Demo {

    [RequireComponent(typeof(Animator))]
    public class AnimatorController : MonoBehaviour {

        [SerializeField]
        private string horizontal = "Horizontal";
        [SerializeField]
        private string vertical = "Vertical";

        private int horizontalId;
        private int verticalId;
        private Animator animator;
        private UnityEngine.AI.NavMeshAgent agent;

        private void Awake() {
            horizontalId = Animator.StringToHash(horizontal);
            verticalId = Animator.StringToHash(vertical);
        }

        private void Start() {
            animator = GetComponent<Animator>();
            agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            agent.updatePosition = agent.updateRotation = false;
        }

        public void UpdateAnimParams(float x, float y) {
            animator.SetFloat(horizontalId, x);
            animator.SetFloat(verticalId, y);
        }

        private void OnAnimatorMove() {
            if (!agent.isStopped) {
                var direction = agent.steeringTarget - transform.position;
                direction.y = 0;
                var steeringDirection = GetRelativeDirection(direction);
                var steer = steeringDirection * NormalizeSteerAngle();
                
                var rotation = Vector3.RotateTowards(transform.forward, direction, Time.deltaTime, 0f);
                rotation.y = 0;
                transform.rotation = Quaternion.LookRotation(rotation);

                transform.position = transform.position + animator.deltaPosition;
                agent.nextPosition = transform.position;

                UpdateAnimParams(steer, 1f);
            } else {
                UpdateAnimParams(0, 0);
            }
        }

        /**
         * Returns -1 if the position is left relative to the current position,
         * 1 if the position is right of the current position.
         */
        private int GetRelativeDirection(Vector3 direction) {
            var cross = Vector3.Cross(transform.forward, direction);
            var magnitude = Vector3.Dot(cross, Vector3.up);

            return magnitude < 0 ? -1 : 1;
        }
        
        /**\
         * Maps a range of 0 degrees to 180 degrees between 0 and 1 to pass into the horizontal/vertical
         * parameters of the animator.
         */
        private float NormalizeSteerAngle() {
            var dir = agent.nextPosition - transform.position;
            dir.y = 0;
            var angle = Vector3.Angle(transform.forward, dir);

            Debug.DrawRay(transform.position, dir * 100, Color.red);

            return angle / 180f;
        }
    }
}

