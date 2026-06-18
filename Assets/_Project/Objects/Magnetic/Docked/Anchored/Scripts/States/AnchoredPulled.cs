using ProjectConnections.Magnetism.Modules;
using ProjectConnections.Magnetism.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetism.Anchored.States
{
    public class AnchoredPulled : IState, StateDockedModule
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
                context.SetState(new AnchoredPulling());
            }
        }

        public void Demagnetize(IContext context) { }

        public void MagnetizeDock(IContext context)
        {
            context.Mover.UsePreciseArrival(true);
            if (context is DockedModule anchorModule)
            {
                context.Mover.MoveTo(anchorModule.OriginalPosition);
            }
            context.SetState(new AnchoredReturning());
        }

        public void DemagnetizeDock(IContext context) { }
    }
}
