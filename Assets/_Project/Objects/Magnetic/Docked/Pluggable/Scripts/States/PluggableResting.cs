using ProjectConnections.Magnetism.Modules;
using ProjectConnections.Magnetism.States;
using UnityEngine;

namespace ProjectConnections.Magnetism.Pluggable.States
{
    public class PluggableResting : IState, StateDockedModule
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
            context.Mover.UseCollision(true);
            context.Mover.UsePreciseArrival(false);
            context.Mover.MoveTo(destination);
            context.SetState(new PluggablePulling());
        }

        public void Demagnetize(IContext context) { }

        public void MagnetizeDock(IContext context) { }

        public void DemagnetizeDock(IContext context) { }
    }
}
