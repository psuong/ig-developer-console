using UnityEngine;

namespace DeveloperConsole {
    
    [RequireComponent(typeof(Camera))]
    public class CameraPointer : MonoBehaviour {
        
        [SerializeField]
        private string mouseInputName = "Fire1";
        [SerializeField, Tooltip("Which layers should be selectively ignored? By default, every layer is selected.")]
        private LayerMask layerMask = -1;

        private Camera camera;

        private void Start() {
            camera = GetComponent<Camera>();
        }

        private void Update() {
            // Get the mouse input
            if (Input.GetButtonUp(mouseInputName)) {
                GetGameObject();
            }
        }

        public GameObject GetGameObject() {
            var ray = camera.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit)) {
                Debug.LogFormat("Got {0}", hit.collider.name);
                return hit.collider.gameObject;
            }

#if UNITY_EDITOR
            Debug.DrawRay(ray.origin, ray.direction, Color.green);
#endif
            return null;
        }

    }

}
