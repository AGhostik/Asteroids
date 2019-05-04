using System;
using UnityEngine;

namespace Resources.Core {
    public class GameStage : IGameStage {
        public GameStage(IGameScore gameScore, ISpawner spawner, IMainObjectsSource mainObjectsSource) {
            _gameScore = gameScore;
            _spawner = spawner;
            _mainObjectsSource = mainObjectsSource;
        }

        public Action PlayerDefeatedCallback { get; set; }

        private readonly IGameScore _gameScore;
        private readonly IMainObjectsSource _mainObjectsSource;
        private readonly ISpawner _spawner;

        public void Exit() {
            Application.Quit();
        }

        public void PlayerDefeated() {
            PlayerDefeatedCallback?.Invoke();
        }

        public void Restart() {
            _spawner.Reset();
            _gameScore.Reset();
            _resetPlayer(_mainObjectsSource.GetPlayer());
        }

        private static void _resetPlayer(GameObject player) {
            player.SetActive(true);
            player.transform.position = Vector3.zero;
            player.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}