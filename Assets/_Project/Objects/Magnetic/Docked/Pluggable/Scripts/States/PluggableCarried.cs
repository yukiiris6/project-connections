using ProjectConnections.Magnetic.Modules;
using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.Pluggable.States
{
    public class PluggableCarried : IState, StateDockedModule
    {
        IContext _context;

        public void Enter(IContext context)
        {
            _context = context;
            context.Mover.Stop();
            context.Mover.ResetRotation();

            if (context is CarriableModule carriableModule)
            {
                carriableModule.CarriableObject.OnCarryChanged += HandleCarryChanged;
            }
        }

        public void Exit(IContext context)
        {
            _context = context;
            if (context is CarriableModule carriableModule)
            {
                carriableModule.CarriableObject.OnCarryChanged -= HandleCarryChanged;
            }
        }

        public void Magnetize(IContext context, Vector2 destination) { }

        public void Demagnetize(IContext context) { }

        public void MagnetizeDock(IContext context) { }

        public void DemagnetizeDock(IContext context) { }

        void HandleCarryChanged(bool value)
        {
            if (!value) _context.SetState(new PluggablePulled());
        }
    }
}
