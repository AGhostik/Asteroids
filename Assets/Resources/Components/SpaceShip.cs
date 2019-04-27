using UnityEngine;
using Zenject;

namespace Resources.Components {
    public class SpaceShip : MonoBehaviour {
        public float moveAcceleration = 150;
        public float rotationAcceleration = 150;
        public float maxVelocityMagnitude = 3;
        public float brakingMultiplier = 2f;

        private IController _controller;
        private Rigidbody2D _rigidbody2D;
        private Transform _transform;

        private void Awake() {
            _transform = GetComponent<Transform>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate() {
            if (_controller.Up()) {
                _rigidbody2D.AddForce(_transform.up * moveAcceleration * Time.fixedDeltaTime);
                if (_rigidbody2D.velocity.magnitude > maxVelocityMagnitude) {
                    _rigidbody2D.velocity = _rigidbody2D.velocity.normalized * maxVelocityMagnitude;
                }
            }

            if (_controller.Down()) {
                // торможение
                if (_rigidbody2D.velocity.magnitude > 0) {
                    var velocity = _rigidbody2D.velocity;
                    velocity -= velocity * brakingMultiplier * Time.fixedDeltaTime;
                    _rigidbody2D.velocity = velocity;
                } else {
                    _rigidbody2D.velocity = Vector2.zero;
                }
            }

            var rotation = 0f;
            if (_controller.Left()) {
                rotation = rotationAcceleration;
            }

            if (_controller.Right()) {
                rotation = -rotationAcceleration;
            }

            _rigidbody2D.MoveRotation(_rigidbody2D.rotation + rotation * Time.fixedDeltaTime);
        }

        [Inject]
        private void _init(IController controller) {
            _controller = controller;
        }
    }
}