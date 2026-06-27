using ProjectConnections.Magnetic.Modules;
using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.Pluggable.States
{
    public class PluggableReturning : IState, StateAnchorModule
    {
        IContext _context;

        public void Enter(IContext context)
        {
            if (context is not PlugModule plugModule) return;
            _context = context;
            context.Mover.OnDestinationReached += OnArrival;
            plugModule.PlugCarryRange.SetReturning(true);
        }

        public void Exit(IContext context)
        {
            if (context is not PlugModule plugModule) return;
            context.Mover.OnDestinationReached -= OnArrival;
            plugModule.PlugCarryRange.SetReturning(false);
        }

        public void DemagnetizeAnchor(IContext context)
        {
            context.Mover.Stop();
            context.SetState(new PluggablePulled());
        }

        void OnArrival()
        {
            float distance = _context.Mover.GetDistanceTravelled();
            _context.Presenter.PlayStopByDistance(distance);
            _context.SetState(new PluggableResting());
        }

        public void Magnetize(IContext context, Vector2 destination) { }
        public void Demagnetize(IContext context) { }
        public void MagnetizeAnchor(IContext context) { }
    }
}
