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
        public float spawnFieldWidth = 1f;
        public SpawnEnemy[] spawnEnemies;
        private float[] _enemiesTimer;

        private IMainObjectsSource _mainObjectsSource;

        /// <summary>
        ///     Зоны вокруг области видимости, в которых появляются враги
        /// </summary>
        private Area[] _spawnAreas;
        private ISpawner _spawner;

        private void Awake() {
            var visibleArea = _mainObjectsSource.GetVisibleArea();
            _spawnAreas = _createAreasAroundVisible(visibleArea, spawnFieldWidth);

            _enemiesTimer = new float[spawnEnemies.Length];
        }

        private void Update() {
            for (var index = 0; index < spawnEnemies.Length; index++) {
                var spawnEnemy = spawnEnemies[index];
                if (_enemiesTimer[index] <= 0) {
                    _spawn(spawnEnemy.enemy, spawnEnemy.countLimit);
                    _enemiesTimer[index] = spawnEnemy.timeBetweenSpawns;
                } else {
                    _enemiesTimer[index] -= Time.deltaTime;
                }
            }
        }

        private static Area[] _createAreasAroundVisible(Area visibleArea, float width) {
            var leftArea = new Area(new Vector2(visibleArea.LeftUpPoint.x - width, visibleArea.LeftUpPoint.y), visibleArea.LeftDownPoint);
            var topArea = new Area(new Vector2(visibleArea.LeftUpPoint.x, visibleArea.LeftUpPoint.y - width), visibleArea.RightUpPoint);
            var rightArea = new Area(visibleArea.RightUpPoint, new Vector2(visibleArea.RightDownPoint.x + width, visibleArea.RightDownPoint.y));
            var bottomArea = new Area(visibleArea.LeftDownPoint, new Vector2(visibleArea.RightDownPoint.x, visibleArea.RightDownPoint.y + width));

            return new[] {leftArea, topArea, rightArea, bottomArea};
        }

        [Inject]
        private void _init(ISpawner spawner, IMainObjectsSource mainObjectsSource) {
            _spawner = spawner;
            _mainObjectsSource = mainObjectsSource;
        }

        private void _spawn(GameObject enemy, int enemyCountLimit) {
            if (!_spawner.TrySpawn(enemy, enemyCountLimit, out var nextEnemy)) {
                return;
            }

            var areaIndex = Random.Range(0, _spawnAreas.Length);
            var area = _spawnAreas[areaIndex];

            nextEnemy.transform.position = area.GetRandomPoint();

            nextEnemy.SetActive(true);
        }
    }
}