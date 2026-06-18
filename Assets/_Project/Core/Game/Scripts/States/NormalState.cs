using UnityEngine;
using Sirenix.OdinInspector;

public class NormalState : GameState
{
    public void Pause(GameContext context)
    {
        context.SetState(new PausedState());
        context.TimeController.PauseTime();
    }

    public void Resume(GameContext context) { }

    public void Slowdown(GameContext context) { }
}