using System;

namespace Resources.Core {
    public interface IGameStage {
        event Action PlayerDefeatedEvent;
        void Exit();
        void PlayerDefeated();
        void Restart();
    }
}