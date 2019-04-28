using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Resources.Components {
    [Serializable]
    public class Drop {
        //public int minCount = 1;
        //public int maxCount = 3;
        public int count;
        public GameObject gameObject;
    }

    public class Asteroid : MonoBehaviour {
        public int score;
        public Drop[] drops;

        private bool _canDissapear;
        private Destroyable _destroyable;
        private IDropSpawner _dropSpawner;
        private IGameScore _gameScore;
        private Transform _transform;

        private void Awake() {
            _destroyable = GetComponent<Destroyable>();
            _destroyable.afterDestroy = _afterDestroy;

            _transform = GetComponent<Transform>();
        }

        private void OnBecameInvisible() {
            if (_canDissapear) {
                gameObject.SetActive(false);
            }
        }

        private void OnBecameVisible() {
            _canDissapear = true;
        }

        private void _afterDestroy(Collider2D obj) {
            _gameScore.Increase(score);
            foreach (var drop in drops) {
                var dropObjects = _dropSpawner.GetDrop(drop.gameObject, drop.count);
                foreach (var dropObject in dropObjects) {
                    dropObject.transform.position = _transform.position;
                    dropObject.transform.rotation = new Quaternion(0, 0, Random.rotation.z, Random.rotation.w);
                    dropObject.SetActive(true);
                }
            }
        }

        [Inject]
        private void _init(IGameScore gameScore, IDropSpawner dropSpawner) {
            _gameScore = gameScore;
            _dropSpawner = dropSpawner;
        }
    }
}