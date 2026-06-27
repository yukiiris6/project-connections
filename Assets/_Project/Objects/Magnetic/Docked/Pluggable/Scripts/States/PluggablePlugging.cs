using ProjectConnections.Magnetic.Modules;
using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.Pluggable.States
{
    public class PluggablePlugging : IState, StateAnchorModule
    {
        IContext _context;

        public void Enter(IContext context)
        {
            _context = context;
            context.Mover.OnDestinationReached += OnArrival;
        }

        public void Exit(IContext context)
        {
            context.Mover.OnDestinationReached -= OnArrival;
        }

        void OnArrival()
        {
            _context.SetState(new PluggablePlugged());
        }

        public void Magnetize(IContext context, Vector2 destination) { }
        public void Demagnetize(IContext context) { }
        public void MagnetizeAnchor(IContext context) { }
        public void DemagnetizeAnchor(IContext context) { }
    }
}
