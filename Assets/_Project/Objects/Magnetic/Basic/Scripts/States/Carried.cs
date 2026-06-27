using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.Magnetic.Modules;

namespace ProjectConnections.Magnetic.States
{
    public class Carried : IState
    {
        IContext _context;

        public void Enter(IContext context)
        {
            if (context is not CarriableModule carriableModule) return;
            _context = context;
            context.Mover.Stop();
            carriableModule.CarriableObject.OnCarryChanged += HandleCarryChanged;
        }

        public void Exit(IContext context)
        {
            if (context is not CarriableModule carriableModule) return;
            carriableModule.CarriableObject.OnCarryChanged -= HandleCarryChanged;
        }

        void HandleCarryChanged(bool value)
        {
            if (!value) _context.SetState(new Resting());
        }

        public void Magnetize(IContext context, Vector2 destination) { }

        public void Demagnetize(IContext context) { }
    }
}
