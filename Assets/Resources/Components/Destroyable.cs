using System;
using UnityEngine;

namespace Resources.Components {
    public class Destroyable : MonoBehaviour {
        public Action<Collider2D> afterDestroy;

        private void OnTriggerEnter2D(Collider2D other) {
            gameObject.SetActive(false);

            afterDestroy?.Invoke(other);
        }
    }
}