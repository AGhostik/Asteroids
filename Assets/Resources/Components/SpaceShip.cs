using Resources.Core;
using UnityEngine;
using Zenject;

namespace Resources.Components {
    public class SpaceShip : MonoBehaviour {
        public float moveAcceleration = 150;
        public float rotationAcceleration = 150;
        public float maxVelocityMagnitude = 3;
        public float brakingMultiplier = 2f;
        private IController _controller;

        private IGameStage _gameStage;
        private Rigidbody2D _rigidbody2D;
        private Transform _transform;

        private void Awake() {
            _transform = GetComponent<Transform>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate() {
            if (_controller.Up()) {
                _acceleration();
            }

            if (_controller.Down()) {
                _brake();
            }

            if (_controller.Left() &&
                !_controller.Right()) {
                _rigidbody2D.MoveRotation(_rigidbody2D.rotation + rotationAcceleration * Time.fixedDeltaTime);
            }

            if (_controller.Right()) {
                _rigidbody2D.MoveRotation(_rigidbody2D.rotation - rotationAcceleration * Time.fixedDeltaTime);
            }
        }

        private void OnDisable() {
            _gameStage.PlayerDefeated();
        }

        private void _acceleration() {
            _rigidbody2D.AddForce(_transform.up * moveAcceleration * Time.fixedDeltaTime);
            if (_rigidbody2D.velocity.magnitude > maxVelocityMagnitude) {
                _rigidbody2D.velocity = _rigidbody2D.velocity.normalized * maxVelocityMagnitude;
            }
        }

        private void _brake() {
            if (_rigidbody2D.velocity.magnitude > 0) {
                var velocity = _rigidbody2D.velocity;
                velocity -= velocity * brakingMultiplier * Time.fixedDeltaTime;
                _rigidbody2D.velocity = velocity;
            } else {
                _rigidbody2D.velocity = Vector2.zero;
            }
        }

        [Inject]
        private void _init(IController controller, IGameStage gameStage) {
            _controller = controller;
            _gameStage = gameStage;
        }
    }
}