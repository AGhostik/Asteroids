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

        private void Awake() {
            _gameStage.PlayerDefeatedEvent += _playerDefeatedCallback;
        }

        private void OnDestroy() {
            _gameStage.PlayerDefeatedEvent -= _playerDefeatedCallback;
        }

        [Inject]
        private void _init(IGameStage gameStage, IGameScore gameScore) {
            _gameStage = gameStage;
            _gameScore = gameScore;
        }

        private void _playerDefeatedCallback() {
            if (gameOverText.IsDestroyed()) {
                // исправляет ошибку при закрытии
                // MissingReferenceException: The object of type 'Text' has been destroyed but you are still trying to access it
                return;
            }

            gameOverText.text = $"GameOver{Environment.NewLine}Score: {_gameScore.GetScore()}{Environment.NewLine}Restart? Y/N";
            // если включать/выключать только компонент Text, а gameObject сделать постоянно активным
            // то будет всегда работать компонент EventTrigger, из-за чего нажатие на Submit и Cancel будет срабатывать даже когда корабль не уничтожен
            // проще включать/выключать сам gameObject
            gameOverText.gameObject.SetActive(true);
        }
    }
}