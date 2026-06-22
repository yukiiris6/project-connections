using ProjectConnections.Magnetic.Modules;
using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.Elastic.States
{
    public class ElasticResting : IState
    {
        public void Enter(IContext context)
        {
            if (context is DockedModule anchorModule)
            {
                context.Mover.SnapTo(anchorModule.OriginalPosition);
            }
            context.SoundPlayer.PlayCrashSFX();
        }

        public void Exit(IContext context) { }

        public void Magnetize(IContext context, Vector2 destination)
        {
            context.Mover.UsePreciseArrival(false);
            context.Mover.MoveTo(destination);
            context.SetState(new ElasticPulling());
        }

        public void Demagnetize(IContext context) { }
    }
}
