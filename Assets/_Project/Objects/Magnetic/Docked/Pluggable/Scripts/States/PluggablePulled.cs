using ProjectConnections.Electric;
using ProjectConnections.Magnetic.Anchored.States;
using ProjectConnections.Magnetic.Modules;
using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.Pluggable.States
{
    public class PluggablePulled : IState, StateDockedModule
    {
        public void Enter(IContext context)
        {
            context.SoundPlayer.PlayCrashSFX();
            context.Presenter.PlayShake();
            if (context is EnergizerModule energizerModule)
            {
                SocketConnector socketConnector = energizerModule.Energizer.SearchFreeSocket();
                if (socketConnector)
                {
                    context.Mover.UsePreciseArrival(true);
                    context.Mover.MoveTo(socketConnector.ConnectionAnchor.position);
                    context.Mover.RotateTowardsTarget();
                    context.SetState(new PluggablePlugging());
                }
            }
        }

        public void Exit(IContext context) { }

        public void Magnetize(IContext context, Vector2 destination)
        {
            bool isSameAsCurrentPosition = context.Mover.IsSameAsCurrentPosition(destination);
            if (!isSameAsCurrentPosition)
            {
                context.Mover.UseCollision(false);
                context.Mover.UsePreciseArrival(false);
                context.Mover.MoveTo(destination);
                context.Mover.RotateTowardsTarget();
                context.SetState(new PluggablePulling());
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
            context.Mover.ResetRotation();
            context.SetState(new PluggableReturning());
        }

        public void DemagnetizeDock(IContext context) { }
    }
}
