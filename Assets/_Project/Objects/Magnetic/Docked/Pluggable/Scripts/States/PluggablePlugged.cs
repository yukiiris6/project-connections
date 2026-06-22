using ProjectConnections.Magnetic.Modules;
using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.Pluggable.States
{
    public class PluggablePlugged : IState, StateDockedModule
    {
        public void Enter(IContext context)
        {
            if (context is EnergizerModule energizerModule)
            {
                Vector2? connectionPosition = energizerModule.Energizer.GetConnectionPosition();
                Quaternion? connectionRotation = energizerModule.Energizer.GetConnectionRotation();
                energizerModule.Energizer.Energize();
                if (connectionPosition is Vector2 snapPosition)
                {
                    context.Mover.SnapTo(snapPosition);
                    context.SoundPlayer.PlayCrashSFX();
                    context.SoundPlayer.PlayConnectionSFX();
                    context.Presenter.PlayShake();
                    if (connectionRotation is Quaternion rotation)
                    {
                        context.Mover.SetRotation(rotation);
                    }
                }
            }
        }

        public void Exit(IContext context)
        {
            if (context is EnergizerModule energizerModule)
            {
                energizerModule.Energizer.Deenergize();
            }
        }

        public void Magnetize(IContext context, Vector2 destination)
        {
            bool isSameAsCurrentPosition = context.Mover.IsSameAsCurrentPosition(destination);
            if (!isSameAsCurrentPosition)
            {
                context.Mover.UseCollision(false);
                context.Mover.UsePreciseArrival(false);
                context.Mover.MoveTo(destination);
                context.SetState(new PluggablePulling());
            }
        }

        public void Demagnetize(IContext context) { }

        public void MagnetizeDock(IContext context)
        {
            if (context is DockedModule anchorModule)
            {
                context.Mover.UseCollision(false);
                context.Mover.UsePreciseArrival(true);
                context.Mover.MoveTo(anchorModule.OriginalPosition);
                context.Mover.ResetRotation();
                context.SetState(new PluggableReturning());
            }
        }

        public void DemagnetizeDock(IContext context) { }
    }
}
