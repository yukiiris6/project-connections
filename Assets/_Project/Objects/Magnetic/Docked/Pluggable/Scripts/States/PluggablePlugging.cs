using ProjectConnections.Magnetism.Modules;
using ProjectConnections.Magnetism.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetism.Pluggable.States
{
    public class PluggablePlugging : IState, StateDockedModule
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

        public void Magnetize(IContext context, Vector2 destination) { }

        public void Demagnetize(IContext context) { }

        public void MagnetizeDock(IContext context) { }

        public void DemagnetizeDock(IContext context) { }

        void OnArrival()
        {
            _context.SetState(new PluggablePlugged());
        }
    }
}
