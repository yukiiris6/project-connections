using ProjectConnections.Magnetism.Modules;
using ProjectConnections.Magnetism.States;
using UnityEngine;

namespace ProjectConnections.Magnetism.Elastic.States
{
    public class ElasticPulled : IState
    {
        public void Enter(IContext context)
        {
            context.SoundPlayer.PlayCrashSFX();
            context.Presenter.PlayShake();
        }

        public void Exit(IContext context) { }

        public void Magnetize(IContext context, Vector2 destination)
        {
            bool isSameAsCurrentPosition = context.Mover.IsSameAsCurrentPosition(destination);
            if (!isSameAsCurrentPosition)
            {
                context.Mover.UsePreciseArrival(false);
                context.Mover.MoveTo(destination);
                context.SetState(new ElasticPulling());
            }
        }

        public void Demagnetize(IContext context)
        {
            if (context is DockedModule anchorModule)
            {
                context.Mover.UsePreciseArrival(true);
                context.Mover.MoveTo(anchorModule.OriginalPosition);
            }
            context.SetState(new ElasticReturning());
        }
    }
}