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
        public void Enter(IContext context)
        {
            float distance = context.Mover.GetDistanceTravelled();
            context.Presenter.PlayStopByDistance(distance);
        }

        public void Magnetize(IContext context, Vector2 destination)
        {
            if (context is not AnchorModule dockedModule) return;

            Vector2 constrainedDestination = context.Constrainer.ConstrainStopDistance(destination);
            constrainedDestination = dockedModule.AnchorRange.ConstrainMaxDistance(constrainedDestination);
            bool isSameAsCurrentPosition = context.Mover.IsSameAsCurrentPosition(constrainedDestination);
            if (isSameAsCurrentPosition) return;
            context.Mover.MoveTo(constrainedDestination);
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

        public void Exit(IContext context) { }
        public void DemagnetizeAnchor(IContext context) { }
        public void Demagnetize(IContext context) { }
    }
}
