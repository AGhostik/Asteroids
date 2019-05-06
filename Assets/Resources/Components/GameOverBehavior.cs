using System;
using Resources.Core;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Resources.Components {
    public class GameOverBehavior : MonoBehaviour {
        public Text gameOverText;
        private IGameScore _gameScore;
        private IGameStage _gameStage;

        public void Exit() {
            _gameStage.Exit();
        }

        public void Restart() {
            _gameStage.Restart();
            gameOverText.gameObject.SetActive(false);
        }

        [Inject]
        private void _init(IGameStage gameStage, IGameScore gameScore) {
            _gameStage = gameStage;
            _gameScore = gameScore;
            _gameStage.PlayerDefeatedCallback = _playerDefeatedCallback;
        }

        private void _playerDefeatedCallback() {
            gameOverText.text = $"GameOver{Environment.NewLine}Score: {_gameScore.GetScore()}{Environment.NewLine}Restart? Y/N";
            gameOverText.gameObject.SetActive(true);
        }
    }
}