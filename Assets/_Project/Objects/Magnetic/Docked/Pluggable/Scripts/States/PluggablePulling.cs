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
            if (context is not PlugModule plugModule) return;
            _context = context;
            plugModule.PlugCarryRange.CarriableObject.OnCarryChanged += HandleCarryChanged;
            context.Mover.OnDestinationReached += OnArrival;
        }

        public void Exit(IContext context)
        {
            if (context is not PlugModule plugModule) return;
            plugModule.PlugCarryRange.CarriableObject.OnCarryChanged -= HandleCarryChanged;
            context.Mover.OnDestinationReached -= OnArrival;
        }

        public void Demagnetize(IContext context)
        {
            context.Mover.Stop();
            context.SetState(new PluggablePulled());
        }

        void OnArrival(float distance)
        {
            _context.Presenter.PlayStopByDistance(distance);
            _context.SetState(new PluggablePulled());
        }

        void HandleCarryChanged(bool value)
        {
            if (value) _context.SetState(new PluggableCarried());
        }

        public void Magnetize(IContext context, Vector2 destination) { }
        public void MagnetizeAnchor(IContext context) { }
        public void DemagnetizeAnchor(IContext context) { }
    }
}
