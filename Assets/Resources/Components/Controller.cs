using UnityEngine;

namespace Resources.Components {
    public class Controller : IController {
        public bool Up() {
            return Input.GetKey(KeyCode.W);
        }

        public bool Right() {
            return Input.GetKey(KeyCode.D);
        }

        public bool Left() {
            return Input.GetKey(KeyCode.A);
        }

        public bool Down() {
            return Input.GetKey(KeyCode.S);
        }
    }
}