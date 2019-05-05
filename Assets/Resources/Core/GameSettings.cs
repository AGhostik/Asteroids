using System;

namespace Resources.Core {
    public class GameSettings : IGameSettings {
        public GameView GameView {
            get => _gameView;
            set {
                _gameView = value;
                GameViewChanged?.Invoke();
            }
        }
        private GameView _gameView;

        public event Action GameViewChanged;
    }
}