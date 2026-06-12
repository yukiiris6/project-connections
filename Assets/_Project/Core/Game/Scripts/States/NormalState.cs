using UnityEngine;

public class NormalState : GameState
{
    public void Pause(GameContext context)
    {
        context.TimeController.PauseTime();
        context.SetState(new PausedState());
    }

    public void Resume(GameContext context) { }

    public void Slowdown(GameContext context) { }
}