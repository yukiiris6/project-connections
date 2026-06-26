using ProjectConnections.Magnetic.Modules;
using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.Pluggable.States
{
    public class PluggablePlugged : IState, StateAnchorModule
    {
        public void Enter(IContext context)
        {
            if (context is not PlugModule plugModule) return;
            Vector2? connectionPosition = plugModule.Energizer.GetConnectionPosition();
            Quaternion? connectionRotation = plugModule.Energizer.GetConnectionRotation();
            if (connectionPosition != null) context.Mover.SnapTo(connectionPosition.Value);
            if (connectionRotation != null) context.Rotator.SetRotation(connectionRotation.Value);
            plugModule.Energizer.Energize();
            context.Presenter.PlayConnectEffects();
        }

        public void Exit(IContext context)
        {
            if (context is not PlugModule plugModule) return;
            plugModule.Energizer.Deenergize();
        }

        public void Magnetize(IContext context, Vector2 destination)
        {
            if (context is not AnchorModule dockedModule) return;

            Vector2 targetPosition = dockedModule.AnchorRange.ConstrainTargetPosition(destination);
            bool isSameAsCurrentPosition = context.Mover.IsSameAsCurrentPosition(targetPosition);
            if (isSameAsCurrentPosition) return;

            context.Mover.MoveTo(destination);
            context.SetState(new PluggablePulling());
        }

        public void MagnetizeAnchor(IContext context)
        {
            if (context is not AnchorModule anchorModule) return;
            context.Mover.MoveTo(anchorModule.AnchorRange.GetOriginalPosition());
            context.Rotator.ResetRotation();
            context.SetState(new PluggableReturning());
        }

        public void Demagnetize(IContext context) { }
        public void DemagnetizeAnchor(IContext context) { }
    }
}
