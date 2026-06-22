using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Core
{
    public class PausedState : GameState
    {
        public void Pause(GameContext context) { }

        public void Resume(GameContext context)
        {
            context.TimeController.ResetTime();
            context.SetState(new NormalState());
        }

        public void Slowdown(GameContext context)
        {
            context.TimeController.SlowdownTime();
            context.SetState(new SlowdownState());
        }
    }
}
