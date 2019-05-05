using System;

namespace Resources.Core {
    public interface IGameSettings {
        GameView GameView { get; set; }
        event Action GameViewChanged;
    }
}