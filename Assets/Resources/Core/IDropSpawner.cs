using System.Collections.Generic;
using UnityEngine;

namespace Resources.Core {
    public interface IDropSpawner {
        List<GameObject> GetDrop(GameObject prefab, int count = 1);
    }
}