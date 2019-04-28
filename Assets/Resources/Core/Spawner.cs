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
        private readonly Dictionary<string, List<GameObject>> _cache = new Dictionary<string, List<GameObject>>();
        private readonly Lazy<Transform> _instancesContainer = new Lazy<Transform>(() => {
            var container = new GameObject("EntitiesContainer");
            return container.GetComponent<Transform>();
        });

        private DiContainer _container;
        
        public bool TrySpawn(GameObject prefab, int limit, out GameObject instanse) {
            instanse = null;
            var prefabName = prefab.name;

            if (_cache.TryGetValue(prefabName, out var instances)) {
                instanse = instances.FirstOrDefault(_ => !_.activeInHierarchy);
            }

            if (instanse == null) {
                if (instances == null ||
                    instances.Count < limit) {
                    instanse = _container.InstantiatePrefab(prefab, _instancesContainer.Value);
                } else {
                    return false;
                }
            }

            if (!_cache.ContainsKey(prefabName)) {
                _cache.Add(prefabName, new List<GameObject>() {instanse});
            } else {
                _cache[prefabName].Add(instanse);
            }

            return true;
        }

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
                _cache.Add(prefabName, new List<GameObject>() {instance});
            } else {
                _cache[prefabName].Add(instance);
            }

            return instance;
        }

        public List<GameObject> Spawn(GameObject prefab, int count) {
            var result = new List<GameObject>();
            var prefabName = prefab.name;

            if (_cache.TryGetValue(prefabName, out var instances)) {
                result = instances.Where(_ => !_.activeInHierarchy).Take(count).ToList();
            }

            while (result.Count < count) {
                var instance = _container.InstantiatePrefab(prefab, _instancesContainer.Value);
                result.Add(instance);
            }

            if (!_cache.ContainsKey(prefabName)) {
                _cache.Add(prefabName, result);
            } else {
                _cache[prefabName].AddRange(result);
            }

            return result;
        }

        [Inject]
        private void _init(DiContainer container) {
            _container = container;
        }
    }
}