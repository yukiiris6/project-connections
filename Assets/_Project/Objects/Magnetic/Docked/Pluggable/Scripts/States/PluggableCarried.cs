using ProjectConnections.Magnetic.Modules;
using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.Pluggable.States
{
    public class PluggableCarried : IState, StateAnchorModule
    {
        IContext _context;

        public void Enter(IContext context)
        {
            if (context is not PlugModule plugModule) return;
            _context = context;
            context.Mover.Stop();
            plugModule.PlugCarryRange.CarriableObject.OnCarryChanged += HandleCarryChanged;
        }

        public void Exit(IContext context)
        {
            if (context is not PlugModule plugModule) return;
            plugModule.PlugCarryRange.CarriableObject.OnCarryChanged -= HandleCarryChanged;
        }

        void HandleCarryChanged(bool value)
        {
            if (!value) _context.SetState(new PluggablePulled());
        }

        public void Magnetize(IContext context, Vector2 destination) { }
        public void Demagnetize(IContext context) { }
        public void MagnetizeAnchor(IContext context) { }
        public void DemagnetizeAnchor(IContext context) { }
    }
}
