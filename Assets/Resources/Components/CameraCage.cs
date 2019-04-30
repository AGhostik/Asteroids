using Resources.Core;
using UnityEngine;

namespace Resources.Components {
    public class CameraCage : MonoBehaviour {
        private const float AdditionalSpace = 0.1f;
        public Camera mainCamera;
        public GameObject player;
        private Rigidbody2D _playerRigidbody2D;
        private Transform _playerTransform;
        private Area _visibleArea;

        public void FixedUpdate() {
            // явно не самое эффективное решение
            // как вариант, можно вокруг видимой зоны расположить четыре boxCollider и использовать OnTriggerEnter2D (возможно так будет лучше по производительности)

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

        private void Awake() {
            _visibleArea = _createVisibleArea(mainCamera);
            _playerTransform = player.GetComponent<Transform>();
            _playerRigidbody2D = player.GetComponent<Rigidbody2D>();
        }

        private static Area _createVisibleArea(Camera camera) {
            var cameraLeftUpPoint = camera.ScreenToWorldPoint(new Vector2(0, 0));
            var cameraRightDownPoint = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

            return new Area(cameraLeftUpPoint, cameraRightDownPoint);
        }
    }
}