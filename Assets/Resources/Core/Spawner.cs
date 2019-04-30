using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Resources.Core {
    public class Spawner : ISpawner {
        /// <summary>
        ///     string = gameObject.Name
        /// </summary>
        private readonly Dictionary<string, HashSet<GameObject>> _cache = new Dictionary<string, HashSet<GameObject>>();
        private readonly Lazy<Transform> _instancesContainer = new Lazy<Transform>(() => {
            var container = new GameObject("EntitiesContainer");
            return container.GetComponent<Transform>();
        });

        private DiContainer _container;

        public GameObject Spawn(GameObject prefab) {
            GameObject instance = null;
            var prefabName = prefab.name;

            if (_cache.TryGetValue(prefabName, out var instances)) {
                instance = instances.FirstOrDefault(_ => !_.activeInHierarchy);
            }

            if (instance == null) {
                instance = _container.InstantiatePrefab(prefab, _instancesContainer.Value);
            }

            if (!_cache.ContainsKey(prefabName)) {
                _cache.Add(prefabName, new HashSet<GameObject>() {instance});
            } else {
                _cache[prefabName].Add(instance);
            }

            return instance;
        }

        public HashSet<GameObject> Spawn(GameObject prefab, int count) {
            var result = new HashSet<GameObject>();
            var prefabName = prefab.name;

            if (!_cache.ContainsKey(prefabName)) {
                _cache.Add(prefabName, result);
            }

            if (_cache.TryGetValue(prefabName, out var instances)) {
                var inactive = instances.Where(_ => !_.activeInHierarchy).Take(count).ToArray();

                foreach (var gameObject in inactive) {
                    result.Add(gameObject);
                }

                for (var index = result.Count; index < count; index++) {
                    var instance = _container.InstantiatePrefab(prefab, _instancesContainer.Value);
                    result.Add(instance);
                }
            }

            return result;
        }

        public bool TrySpawn(GameObject prefab, int limit, out GameObject instanse) {
            instanse = null;
            var prefabName = prefab.name;

            if (_cache.TryGetValue(prefabName, out var instances)) {
                if (instances.Count(_ => _.activeInHierarchy) >= limit) {
                    return false;
                }

                instanse = instances.FirstOrDefault(_ => !_.activeInHierarchy);
                if (instanse != null) {
                    return true;
                }

                instanse = _container.InstantiatePrefab(prefab, _instancesContainer.Value);
                instances.Add(instanse);
            } else {
                instanse = _container.InstantiatePrefab(prefab, _instancesContainer.Value);
                _cache.Add(prefabName, new HashSet<GameObject>() {instanse});
            }

            return true;
        }

        [Inject]
        private void _init(DiContainer container) {
            _container = container;
        }
    }
}