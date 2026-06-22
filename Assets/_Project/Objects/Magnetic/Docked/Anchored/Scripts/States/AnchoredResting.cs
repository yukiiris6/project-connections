using ProjectConnections.Magnetic.Modules;
using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.Anchored.States
{
    public class AnchoredResting : IState, StateDockedModule
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
            context.SetState(new AnchoredPulling());
        }

        public void Demagnetize(IContext context) { }

        public void MagnetizeDock(IContext context) { }

        public void DemagnetizeDock(IContext context) { }
    }
}
