using System.Collections.Generic;
using UnityEngine;

namespace Resources.Core {
    public interface ISpawner {
        /// <summary>
        /// Возвращает неактивный инстанс
        /// </summary>
        /// <param name="prefab"></param>
        /// <returns></returns>
        GameObject Spawn(GameObject prefab);

        /// <summary>
        /// Возвращает несколько неактивных инстансов
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        List<GameObject> Spawn(GameObject prefab, int count);

        /// <summary>
        /// Возвращает в out неактивный инстанс, если не был превышен лимит
        /// </summary>
        /// <param name="prefab"></param>
        /// <param name="limit"></param>
        /// <param name="instanse"></param>
        /// <returns>False - лимит был превышен, Out = null</returns>
        bool TrySpawn(GameObject prefab, int limit, out GameObject instanse);
    }
}