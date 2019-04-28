using UnityEngine;

namespace Resources.Core {
    public class GameScore : IGameScore {
        private int _score;

        public void Increase(int value) {
            _score += value;
            Debug.Log(_score);
        }

        public int GetScore() {
            return _score;
        }
    }
}