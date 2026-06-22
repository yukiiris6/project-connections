using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Core
{
    public class SlowdownState : GameState
    {
        public void Pause(GameContext context)
        {
            context.TimeController.PauseTime();
            context.SetState(new PausedState());
        }
        public void Resume(GameContext context)
        {
            context.TimeController.ResetTime();
            context.SetState(new NormalState());
        }
        public void Slowdown(GameContext context) { }
    }
}
