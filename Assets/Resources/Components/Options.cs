using Resources.Core;
using UnityEngine;
using Zenject;

namespace Resources.Components {
    public class Options : MonoBehaviour {
        private IController _controller;
        private IGameSettings _gameSettings;

        private void Update() {
            if (_controller.View1()) {
                _gameSettings.GameView = GameView.Sprite;
            }

            if (_controller.View2()) {
                _gameSettings.GameView = GameView.ThreeD;
            }
        }

        [Inject]
        private void _init(IController controller, IGameSettings gameSettings) {
            _controller = controller;
            _gameSettings = gameSettings;
        }
    }
}