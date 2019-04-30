using Resources.Core;
using UnityEngine;
using Zenject;

namespace Resources.Components {
    public class Ufo : MonoBehaviour {
        public float moveSpeed = 2f;

        private IMainObjectsSource _mainObjectsSource;
        private Transform _playerTransform;
        private Rigidbody2D _rigidbody2D;

        private Transform _transform;

        private void Awake() {
            var player = _mainObjectsSource.GetPlayer();
            _playerTransform = player.GetComponent<Transform>();

            _transform = GetComponent<Transform>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate() {
            Vector2 targetPosition = _playerTransform.position - _transform.position;
            _rigidbody2D.MovePosition(_rigidbody2D.position + targetPosition.normalized * moveSpeed * Time.fixedDeltaTime);
        }

        [Inject]
        private void _init(IMainObjectsSource mainObjectsSource) {
            _mainObjectsSource = mainObjectsSource;
        }
    }
}