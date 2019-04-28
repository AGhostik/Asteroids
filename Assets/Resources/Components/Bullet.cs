using UnityEngine;

namespace Resources.Components {
    public class Bullet : MonoBehaviour {
        // здесь можно добавить дополнительную логику, параметры
        // например, урон

        private void OnBecameInvisible() {
            gameObject.SetActive(false);
        }
    }
}