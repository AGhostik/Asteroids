using System;
using UnityEngine;

namespace Resources.Components {
    public class Destroyable : MonoBehaviour {
        public event Action KillEvent;

        public void Kill() {
            KillEvent?.Invoke();
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            Kill();
        }
    }
}