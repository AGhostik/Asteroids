using UnityEngine;
using Zenject;

namespace Resources.Components {
    public class SpaceShip : MonoBehaviour {
        public float moveSpeed = 150;
        public float rotationSpeed = 70;
        [Range(0.9f, 0.99999f)]
        public float stopMultiplier = 0.98f;

        private IController _controller;
        private Rigidbody2D _rigidbody2D;
        private Transform _transform;

        private void Awake() {
            _transform = GetComponent<Transform>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate() {
            if (_controller.Up()) {
                _rigidbody2D.AddForce(_transform.up * moveSpeed * Time.fixedDeltaTime);
            }

            if (_controller.Down()) {
                // торможение
                _rigidbody2D.velocity *= stopMultiplier;
            }

            var rotation = 0f;
            if (_controller.Left()) {
                rotation = rotationSpeed;
            }

            if (_controller.Right()) {
                rotation = -rotationSpeed;
            }

            _rigidbody2D.MoveRotation(_rigidbody2D.rotation + rotation * Time.fixedDeltaTime);
        }

        [Inject]
        private void _init(IController controller) {
            _controller = controller;
        }
    }
}