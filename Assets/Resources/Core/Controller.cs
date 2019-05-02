using UnityEngine;

namespace Resources.Core {
    public class Controller : IController {
        public bool Down() {
            return Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        }

        public bool Fire1() {
            return Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.RightShift);
        }

        public bool Fire2() {
            return Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.RightControl);
        }

        public bool Left() {
            return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        }

        public bool Right() {
            return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        }

        public bool Up() {
            return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        }
    }
}