using UnityEngine;

namespace Resources.Components {
    public class Ufo : MonoBehaviour {
        public float moveSpeed = 2f;

        private Transform _transform;
        private Transform _playerTransform;
        private Rigidbody2D _rigidbody2D;

        private void Awake() {
            var player = GameObject.Find("Player");
            _playerTransform = player.GetComponent<Transform>();

            _transform = GetComponent<Transform>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }
        
        private void FixedUpdate() {
            Vector2 targetPosition = _playerTransform.position - _transform.position;
            _rigidbody2D.MovePosition(_rigidbody2D.position + targetPosition.normalized * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
