using System;

namespace Resources.Core {
    public interface IGameStage {
        string GameOverText { get; }
        Action PlayerDefeatedCallback { get; set; }
        void Exit();
        void PlayerDefeated();
        void Restart();
    }
}