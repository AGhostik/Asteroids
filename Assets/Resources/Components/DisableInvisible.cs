using System.Linq;
using UnityEngine;

namespace Resources.Components {
    public class DisableInvisible : MonoBehaviour {
        public Renderer[] renders;

        private bool _canDissapear;

        private void Update() {
            if (_canDissapear) {
                if (!renders.Any(render => render.isVisible)) {
                    _canDissapear = false;
                    gameObject.SetActive(false);
                }
            } else {
                if (renders.Any(render => render.isVisible)) {
                    _canDissapear = true;
                }
            }
        }
    }
}