using System;
using Resources.Core;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Resources.Components {
    [Serializable]
    public class SpawnEnemy {
        public float timeBetweenSpawns;
        public int countLimit;
        public GameObject enemy;
    }

    public class EnemySpawner : MonoBehaviour {
        public Camera mainCamera;
        public float spawnFieldWidth = 3f;
        public float timeBetweenSpawns = 5f;
        public int enemyCountLimit = 10;
        public GameObject enemy;

        private Area[] _areas;
        private float _nextSpawnTimeLeft;
        private ISpawner _spawner;
        private Area _visibleArea;

        private void Awake() {
            _areas = _createZonesBeyondCameraVisibility();
            _visibleArea = _createVisibleArea();
        }

        private void Update() {
            if (_nextSpawnTimeLeft <= 0) {
                _spawn();
                _nextSpawnTimeLeft = timeBetweenSpawns;
            } else {
                _nextSpawnTimeLeft -= Time.deltaTime;
            }

#if DEBUG
            _areas[0].DebugDraw(Color.red);
            _areas[1].DebugDraw(Color.green);
            _areas[2].DebugDraw(Color.blue);
            _areas[3].DebugDraw(Color.yellow);
            _visibleArea.DebugDraw(Color.cyan);
#endif
        }

        [Inject]
        private void _init(ISpawner spawner) {
            _spawner = spawner;
        }

        private void _spawn() {
            if (!_spawner.TrySpawn(enemy, enemyCountLimit, out var nextEnemy)) {
                return;
            }

            var areaIndex = Random.Range(0, _areas.Length);
            var area = _areas[areaIndex];

            nextEnemy.transform.position = area.GetRandomPoint();
            _lookAt2D(nextEnemy.transform, _visibleArea.GetRandomPoint());

            nextEnemy.SetActive(true);
        }

        private static void _lookAt2D(Transform transform2D, Vector2 point) {
            // можно сделать методом расширения

            var position = transform2D.position;
            var direction = new Vector2(point.x - position.x, point.y - position.y);
            transform2D.up = direction;
        }

        private Area _createVisibleArea() {
            var cameraLeftUpPoint = mainCamera.ScreenToWorldPoint(new Vector2(0, 0));
            var cameraRightDownPoint = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

            return new Area(cameraLeftUpPoint, cameraRightDownPoint);
        }

        private Area[] _createZonesBeyondCameraVisibility() {
            var cameraLeftUpPoint = mainCamera.ScreenToWorldPoint(new Vector2(0, 0));
            var cameraLeftDownPoint = mainCamera.ScreenToWorldPoint(new Vector2(0, Screen.height));
            var cameraRightUpPoint = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, 0));
            var cameraRightDownPoint = mainCamera.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

            var leftArea = new Area(new Vector2(cameraLeftUpPoint.x - spawnFieldWidth, cameraLeftUpPoint.y), cameraLeftDownPoint);
            var topArea = new Area(new Vector2(cameraLeftUpPoint.x, cameraLeftUpPoint.y - spawnFieldWidth), cameraRightUpPoint);
            var rightArea = new Area(cameraRightUpPoint, new Vector2(cameraRightDownPoint.x + spawnFieldWidth, cameraRightDownPoint.y));
            var bottomArea = new Area(cameraLeftDownPoint, new Vector2(cameraRightDownPoint.x, cameraRightDownPoint.y + spawnFieldWidth));

            return new[] {leftArea, topArea, rightArea, bottomArea};
        }
    }
}