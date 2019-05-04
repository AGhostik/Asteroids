using System;

namespace Resources.Core {
    public interface IGameStage {
        Action PlayerDefeatedCallback { get; set; }
        void Exit();
        void PlayerDefeated();
        void Restart();
    }
}