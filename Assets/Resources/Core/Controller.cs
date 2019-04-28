using UnityEngine;

namespace Resources.Core {
    public class Controller : IController {
        public bool Up() {
            return Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        }

        public bool Fire1() {
            return Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.RightShift); //Input.GetAxisRaw("Fire1") > 0;
        }

        public bool Fire2() {
            return Input.GetAxisRaw("Fire2") > 0;
        }

        public bool Right() {
            return Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
        }

        public bool Left() {
            return Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
        }

        public bool Down() {
            return Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        }
    }
}