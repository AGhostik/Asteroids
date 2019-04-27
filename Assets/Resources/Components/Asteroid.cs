using UnityEngine;

namespace Resources.Components {
    public class Asteroid : MonoBehaviour {
        private Transform _transform;
        private bool _canDissapear;

        private void Awake() {
            _transform = GetComponent<Transform>();
        }

        private void Update() {
        }

        private void OnBecameInvisible() {
            if (_canDissapear) {
                gameObject.SetActive(false);
            }
        }

        private void OnBecameVisible() {
            _canDissapear = true;
        }
    }
}