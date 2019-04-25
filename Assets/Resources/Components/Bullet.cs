using UnityEngine;

namespace Resources.Components {
    public class Bullet : MonoBehaviour {
        private Transform _transform;

        public bool CanLaunch() {
            return !gameObject.activeInHierarchy;
        }

        public void Launch(Vector3 startPosition, Quaternion startRotation) {
            _transform.position = startPosition;
            _transform.rotation = startRotation;

            gameObject.SetActive(true);
        }

        private void Awake() {
            _transform = GetComponent<Transform>();
        }

        private void OnBecameInvisible() {
            gameObject.SetActive(false);
        }
    }
}