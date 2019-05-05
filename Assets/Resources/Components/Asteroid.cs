using Resources.Core;
using UnityEngine;
using Zenject;

namespace Resources.Components {
    public class Asteroid : MonoBehaviour {
        private IMainObjectsSource _mainObjectsSource;
        private Transform _transform;
        private Area _visibleArea;

        private void Awake() {
            _transform = GetComponent<Transform>();
            _visibleArea = _mainObjectsSource.GetVisibleArea();
        }

        private void OnEnable() {
            _lookAt2D(_transform, _visibleArea.GetRandomPoint());
        }

        [Inject]
        private void _init(IMainObjectsSource mainObjectsSource) {
            _mainObjectsSource = mainObjectsSource;
        }

        private static void _lookAt2D(Transform transform2D, Vector2 point) {
            // можно сделать методом расширения

            var position = transform2D.position;
            var direction = new Vector2(point.x - position.x, point.y - position.y);
            transform2D.up = direction;
        }
    }
}