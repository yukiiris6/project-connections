public interface GameContext
{
    TimeController TimeController { get; }
    void SetState(GameState newState);
}