using ProjectConnections.Magnetic.Modules;
using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.Pluggable.States
{
    public class PluggableResting : IState, StateAnchorModule
    {
        public void Enter(IContext context)
        {
            if (context is not AnchorModule anchorModule) return;
            context.Mover.SnapTo(anchorModule.AnchorRange.GetOriginalPosition());
            context.Rotator.ResetRotation();
            context.Presenter.PlayStopEffects();
        }

        public void Magnetize(IContext context, Vector2 destination)
        {
            if (context is not AnchorModule dockedModule) return;
            Vector2 targetPosition = dockedModule.AnchorRange.ConstrainTargetPosition(destination);
            context.Mover.MoveTo(targetPosition);
            context.Rotator.RotateTowardsTarget(destination);
            context.SetState(new PluggablePulling());
        }

        public void Exit(IContext context) { }
        public void Demagnetize(IContext context) { }
        public void MagnetizeAnchor(IContext context) { }
        public void DemagnetizeAnchor(IContext context) { }
    }
}
