using ProjectConnections.Electric;
using ProjectConnections.Magnetic.Anchored.States;
using ProjectConnections.Magnetic.Modules;
using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.Pluggable.States
{
    public class PluggablePulled : IState, StateAnchorModule
    {
        public void Magnetize(IContext context, Vector2 destination)
        {
            if (context is not AnchorModule dockedModule) return;

            Vector2 targetPosition = dockedModule.AnchorRange.ConstrainTargetPosition(destination);
            bool isSameAsCurrentPosition = context.Mover.IsSameAsCurrentPosition(targetPosition);
            if (isSameAsCurrentPosition) return;
            context.Mover.MoveTo(targetPosition);
            context.Rotator.RotateTowardsTarget(destination);
            context.SetState(new PluggablePulling());
        }

        public void MagnetizeAnchor(IContext context)
        {
            if (context is not AnchorModule anchorModule) return;
            context.Mover.MoveTo(anchorModule.AnchorRange.GetOriginalPosition());
            context.Rotator.ResetRotation();
            context.SetState(new PluggableReturning());
        }

        public void Enter(IContext context) { }
        public void Exit(IContext context) { }
        public void DemagnetizeAnchor(IContext context) { }
        public void Demagnetize(IContext context) { }
    }
}
