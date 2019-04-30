using UnityEngine;

namespace Resources.Core {
    public class MainObjectsSource : IMainObjectsSource {
        public MainObjectsSource() {
            _visibleArea = _createVisibleArea(Camera.main);
            _player = GameObject.Find("Player");
        }

        private readonly GameObject _player;
        private readonly Area _visibleArea;

        public GameObject GetPlayer() {
            return _player;
        }

        public Area GetVisibleArea() {
            return _visibleArea;
        }

        private static Area _createVisibleArea(Camera camera) {
            var cameraLeftUpPoint = camera.ScreenToWorldPoint(new Vector2(0, 0));
            var cameraRightDownPoint = camera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

            return new Area(cameraLeftUpPoint, cameraRightDownPoint);
        }
    }
}