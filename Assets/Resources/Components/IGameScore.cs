namespace Resources.Components {
    public interface IGameScore {
        void Increase(int value);
        int GetScore();
    }
}