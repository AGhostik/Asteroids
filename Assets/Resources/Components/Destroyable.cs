using System;
using Resources.Core;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Resources.Components {
    [Serializable]
    public class Drop {
        public int count;
        public GameObject gameObject;
    }

    public class Destroyable : MonoBehaviour {
        public Drop[] drops;

        private ISpawner _spawner;
        private Transform _transform;

        public void Kill() {
            _drop();
            gameObject.SetActive(false);
        }

        private void Awake() {
            _transform = GetComponent<Transform>();
        }

        private void OnTriggerEnter2D(Collider2D other) {
            Kill();
        }

        private void _drop() {
            foreach (var drop in drops) {
                var dropObjects = _spawner.Spawn(drop.gameObject, drop.count);
                foreach (var dropObject in dropObjects) {
                    dropObject.transform.position = _transform.position;
                    dropObject.transform.rotation = new Quaternion(0, 0, Random.rotation.z, Random.rotation.w);
                    dropObject.SetActive(true);
                }
            }
        }

        [Inject]
        private void _init(ISpawner spawner) {
            _spawner = spawner;
        }
    }
}