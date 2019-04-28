using UnityEngine;

namespace Resources.Components {
    public class Area {
        public Area(Vector2 leftUpPoint, Vector2 rightDownPoint) {
            LeftUpPoint = leftUpPoint;
            RightDownPoint = rightDownPoint;

            LeftDownPoint = new Vector2(leftUpPoint.x, rightDownPoint.y);
            RightUpPoint = new Vector2(rightDownPoint.x, leftUpPoint.y);
        }

        public Vector2 RightDownPoint { get; }
        public Vector2 LeftDownPoint { get; }
        public Vector2 RightUpPoint { get; }
        public Vector2 LeftUpPoint { get; }

        public void DebugDraw(Color color) {
            Debug.DrawLine(LeftUpPoint, RightUpPoint, color);
            Debug.DrawLine(RightUpPoint, RightDownPoint, color);
            Debug.DrawLine(RightDownPoint, LeftDownPoint, color);
            Debug.DrawLine(LeftDownPoint, LeftUpPoint, color);

            Debug.DrawLine(LeftUpPoint, RightDownPoint, color);
            Debug.DrawLine(RightUpPoint, LeftDownPoint, color);
        }

        public Vector2 GetRandomPoint() {
            var randomX = Random.Range(LeftUpPoint.x, RightDownPoint.x);
            var randomY = Random.Range(LeftUpPoint.y, RightDownPoint.y);
            return new Vector2(randomX, randomY);
        }
    }
}