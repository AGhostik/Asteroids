namespace Resources.Core {
    public interface IGameScore {
        int GetScore();
        void Increase(int value);
    }
}