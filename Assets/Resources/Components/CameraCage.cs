using Resources.Core;
using UnityEngine;

namespace Resources.Components {
    public class CameraCage : MonoBehaviour {
        public Camera mainCamera;
        public GameObject player;

        private readonly float _additionalSpace = 0.1f;
        private Rigidbody2D _playeRigidbody2D;
        private Transform _playerTransform;
        private Area _visibleArea;

        public void FixedUpdate() {
            var playerPosition = _playerTransform.position;

            if (playerPosition.x < _visibleArea.LeftUpPoint.x) {
                _playeRigidbody2D.MovePosition(new Vector2(_visibleArea.RightDownPoint.x - _additionalSpace, playerPosition.y));
            }

            if (playerPosition.x > _visibleArea.RightDownPoint.x) {
                _playeRigidbody2D.MovePosition(new Vector2(_visibleArea.LeftUpPoint.x + _additionalSpace, playerPosition.y));
            }

            if (playerPosition.y < _visibleArea.LeftUpPoint.y) {
                _playeRigidbody2D.MovePosition(new Vector2(playerPosition.x, _visibleArea.RightDownPoint.y - _additionalSpace));
            }

            if (playerPosition.y > _visibleArea.RightDownPoint.y) {
                _playeRigidbody2D.MovePosition(new Vector2(playerPosition.x, _visibleArea.LeftUpPoint.y + _additionalSpace));
            }
        }

        private void Awake() {
            _visibleArea = _createVisibleArea();
            _playerTransform = player.GetComponent<Transform>();
            _playeRigidbody2D = player.GetComponent<Rigidbody2D>();
        }

        private Area _createVisibleArea() {
            var cameraLeftUpPoint = mainCamera.ScreenToWorldPoint(new Vector2(0, 0));
            var cameraRightDownPoint = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

            return new Area(cameraLeftUpPoint, cameraRightDownPoint);
        }
    }
}