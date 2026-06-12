public interface GameState
{
    void Pause(GameContext context);
    void Resume(GameContext context);
    void Slowdown(GameContext context);
}