using ProjectConnections.Magnetic.Modules;
using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.Pluggable.States
{
    public class PluggablePulling : IState, StateAnchorModule
    {
        IContext _context;

        public void Enter(IContext context)
        {
            if (context is not CarriableModule carriableModule) return;
            _context = context;
            carriableModule.CarriableObject.OnCarryChanged += HandleCarryChanged;
            context.Mover.OnDestinationReached += OnArrival;
        }

        public void Exit(IContext context)
        {
            if (context is not CarriableModule carriableModule) return;
            carriableModule.CarriableObject.OnCarryChanged -= HandleCarryChanged;
            context.Mover.OnDestinationReached -= OnArrival;
        }

        public void Magnetize(IContext context, Vector2 destination)
        {
            if (context is not AnchorModule dockedModule) return;
            Vector2 constrainedDestination = context.Constrainer.ConstrainStopDistance(destination);
            constrainedDestination = dockedModule.AnchorRange.ConstrainMaxDistance(constrainedDestination);
            context.Mover.MoveTo(constrainedDestination);
            context.Rotator.RotateTowardsTarget(destination);
            context.SetState(new PluggablePulling());
        }

        public void Demagnetize(IContext context)
        {
            context.Mover.Stop();
            context.SetState(new PluggablePulled());
        }

        void OnArrival()
        {
            _context.SetState(new PluggablePulled());
        }

        void HandleCarryChanged(bool value)
        {
            if (value) _context.SetState(new PluggableCarried());
        }

        public void MagnetizeAnchor(IContext context) { }
        public void DemagnetizeAnchor(IContext context) { }
    }
}
