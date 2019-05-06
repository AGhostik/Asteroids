using UnityEngine;

namespace Resources.Components {
    [RequireComponent(typeof(Rigidbody2D))]
    public class AutoMoving : MonoBehaviour {
        public float moveSpeed = 5f;
        private Rigidbody2D _rigidbody2D;
        private Transform _transform;

        private void Awake() {
            _transform = GetComponent<Transform>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate() {
            Vector2 direction = _transform.up.normalized * moveSpeed * Time.fixedDeltaTime;
            _rigidbody2D.MovePosition(_rigidbody2D.position + direction);
        }
    }
}