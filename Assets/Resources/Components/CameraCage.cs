using Resources.Core;
using UnityEngine;
using Zenject;

namespace Resources.Components {
    public class CameraCage : MonoBehaviour {
        private const float AdditionalSpace = 0.1f;

        private IMainObjectsSource _mainObjectsSource;
        private Rigidbody2D _playerRigidbody2D;
        private Transform _playerTransform;
        private Area _visibleArea;

        private void Awake() {
            var player = _mainObjectsSource.GetPlayer();
            _playerTransform = player.GetComponent<Transform>();
            _playerRigidbody2D = player.GetComponent<Rigidbody2D>();

            _visibleArea = _mainObjectsSource.GetVisibleArea();
        }

        private void FixedUpdate() {
            // явно не самое эффективное решение
            // как вариант, можно вокруг видимой зоны расположить четыре boxCollider и использовать OnTriggerEnter2D (возможно так будет лучше по производительности)
            // или расположить один boxCollider внутри видимой зоны и использовать OnTriggerExit2D

            var playerPosition = _playerTransform.position;

            if (playerPosition.x < _visibleArea.LeftUpPoint.x) {
                _playerRigidbody2D.MovePosition(new Vector2(_visibleArea.RightDownPoint.x - AdditionalSpace, playerPosition.y));
            }

            if (playerPosition.x > _visibleArea.RightDownPoint.x) {
                _playerRigidbody2D.MovePosition(new Vector2(_visibleArea.LeftUpPoint.x + AdditionalSpace, playerPosition.y));
            }

            if (playerPosition.y < _visibleArea.LeftUpPoint.y) {
                _playerRigidbody2D.MovePosition(new Vector2(playerPosition.x, _visibleArea.RightDownPoint.y - AdditionalSpace));
            }

            if (playerPosition.y > _visibleArea.RightDownPoint.y) {
                _playerRigidbody2D.MovePosition(new Vector2(playerPosition.x, _visibleArea.LeftUpPoint.y + AdditionalSpace));
            }
        }

        [Inject]
        private void _init(IMainObjectsSource mainObjectsSource) {
            _mainObjectsSource = mainObjectsSource;
        }
    }
}