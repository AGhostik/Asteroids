using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace Resources.Core {
    public class DropSpawner : IDropSpawner {
        /// <summary>
        ///     string = gameObject.Name
        /// </summary>
        private readonly Dictionary<string, List<GameObject>> _dropCache = new Dictionary<string, List<GameObject>>();
        private readonly Lazy<Transform> _dropContainer = new Lazy<Transform>(() => {
            var container = new GameObject("DropContainer");
            return container.GetComponent<Transform>();
        });

        private DiContainer _container;

        public List<GameObject> GetDrop(GameObject prefab, int count = 1) {
            var dropObjects = new List<GameObject>();
            var prefabName = prefab.name;

            if (_dropCache.TryGetValue(prefabName, out var drops)) {
                dropObjects = drops.Where(_ => !_.activeInHierarchy).Take(count).ToList();
            }

            while (dropObjects.Count < count) {
                var drop = _container.InstantiatePrefab(prefab, _dropContainer.Value);
                dropObjects.Add(drop);
            }

            if (!_dropCache.ContainsKey(prefabName)) {
                _dropCache.Add(prefabName, dropObjects);
            } else {
                _dropCache[prefabName].AddRange(dropObjects);
            }

            return dropObjects;
        }

        [Inject]
        private void _init(DiContainer container) {
            _container = container;
        }
    }
}