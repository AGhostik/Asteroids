using UnityEngine;

namespace Resources.Components {
    public class Area {
        public Area(Vector2 leftUpPoint, Vector2 rightDownPoint) {
            _leftUpPoint = leftUpPoint;
            _rightDownPoint = rightDownPoint;

            _leftDownPoint = new Vector2(leftUpPoint.x, rightDownPoint.y);
            _rightUpPoint = new Vector2(rightDownPoint.x, leftUpPoint.y);
        }

        private readonly Vector2 _leftUpPoint;
        private readonly Vector2 _rightDownPoint;

        private readonly Vector2 _leftDownPoint;
        private readonly Vector2 _rightUpPoint;

        public Vector2 GetRandomPoint() {
            var randomX = Random.Range(_leftUpPoint.x, _rightDownPoint.x);
            var randomY = Random.Range(_leftUpPoint.y, _rightDownPoint.y);
            return new Vector2(randomX, randomY);
        }

        public void DebugDraw(Color color) {
            Debug.DrawLine(_leftUpPoint, _rightUpPoint, color);
            Debug.DrawLine(_rightUpPoint, _rightDownPoint, color);
            Debug.DrawLine(_rightDownPoint, _leftDownPoint, color);
            Debug.DrawLine(_leftDownPoint, _leftUpPoint, color);

            Debug.DrawLine(_leftUpPoint, _rightDownPoint, color);
            Debug.DrawLine(_rightUpPoint, _leftDownPoint, color);
        }
    }
}