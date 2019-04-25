using System;
using UnityEngine;

namespace Resources.Components
{
    public interface IController {
        bool Up();
        bool Right();
        bool Left();
        bool Down();
    }

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
