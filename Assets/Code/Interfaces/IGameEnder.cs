namespace Project
{
    public interface IGameEnder
    {
        event System.Action<GameEndReason, int> OnGameEnded;
    }
}