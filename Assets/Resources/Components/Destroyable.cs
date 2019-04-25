using UnityEngine;

namespace Resources.Components {
    public class Destroyable : MonoBehaviour {
        private void OnTriggerEnter2D(Collider2D other) {
            gameObject.SetActive(false);
        }
    }
}
