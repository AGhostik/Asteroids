using Resources.Core;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Resources.Components {
    public class GameOverBehavior : MonoBehaviour {
        public Text gameOverText;
        private IGameStage _gameStage;

        public void Exit() {
            _gameStage.Exit();
        }

        public void Restart() {
            _gameStage.Restart();
            gameOverText.gameObject.SetActive(false);
        }

        [Inject]
        private void _init(IGameStage gameStage) {
            _gameStage = gameStage;
            _gameStage.PlayerDefeatedCallback = _playerDefeatedCallback;
        }

        private void _playerDefeatedCallback() {
            gameOverText.text = _gameStage.GameOverText;
            gameOverText.gameObject.SetActive(true);
        }
    }
}