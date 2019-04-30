using Resources.Core;
using UnityEngine;
using Zenject;

namespace Resources.Components {
    public class GetScore : MonoBehaviour {
        public int score;

        private IGameScore _gameScore;

        private void OnDisable() {
            _gameScore.Increase(score);
        }

        [Inject]
        private void _init(IGameScore gameScore) {
            _gameScore = gameScore;
        }
    }
}