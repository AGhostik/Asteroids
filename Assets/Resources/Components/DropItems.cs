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

    [RequireComponent(typeof(Destroyable))]
    public class DropItems : MonoBehaviour {
        public Drop[] drops;

        private Destroyable _destroyable;
        private ISpawner _spawner;
        private Transform _transform;

        private void Awake() {
            _transform = GetComponent<Transform>();
            _destroyable = GetComponent<Destroyable>();

            _destroyable.KillEvent += _destroyableOnKillEvent;
        }

        private void OnDestroy() {
            _destroyable.KillEvent -= _destroyableOnKillEvent;
        }

        private void _destroyableOnKillEvent() {
            _drop();
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