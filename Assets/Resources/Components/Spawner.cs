using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Resources.Components {
    //[System.Serializable]
    //public class Enemy {
    //    public GameObject enemy;
    //}

    public class Spawner : MonoBehaviour {
        public Camera mainCamera;
        public float spawnFieldWidth = 3f;
        public float timeBetweenSpawns = 5f;
        public int enemyCountLimit = 10;
        public GameObject enemy;

        private readonly List<GameObject> _enemiesCached = new List<GameObject>();
        private Area[] _areas;

        private DiContainer _container;
        private Transform _enemyContainer;
        private float _nextSpawnTimeLeft;
        private Area _visibleArea;

        private void Awake() {
            _areas = _createZonesBeyondCameraVisibility();
            _visibleArea = _createVisibleArea();

            var thisTransform = GetComponent<Transform>();
            var bulletContainer = new GameObject("EnemyContainer");
            _enemyContainer = bulletContainer.GetComponent<Transform>();
            _enemyContainer.position = thisTransform.position;
            _enemyContainer.parent = thisTransform;
        }

        private void Update() {
            if (_nextSpawnTimeLeft <= 0 &&
                _enemiesCached.Count < enemyCountLimit) {
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
        private void _init(DiContainer container) {
            _container = container;
        }

        private void _spawn() {
            var areaIndex = Random.Range(0, _areas.Length);
            var area = _areas[areaIndex];

            var nextEnemy = _getNextEnemy();

            nextEnemy.transform.position = area.GetRandomPoint();
            _lookAt2D(nextEnemy.transform, _visibleArea.GetRandomPoint());

            nextEnemy.SetActive(true);
        }

        private GameObject _getNextEnemy() {
            var nextEnemy = _enemiesCached.FirstOrDefault(_ => !_.gameObject.activeInHierarchy);
            if (nextEnemy == null) {
                // InstantiatePrefab нужен для внедрения зависимости в компонентах enemy
                nextEnemy = _container.InstantiatePrefab(enemy, _enemyContainer);
                _enemiesCached.Add(nextEnemy);
            }

            return nextEnemy;
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