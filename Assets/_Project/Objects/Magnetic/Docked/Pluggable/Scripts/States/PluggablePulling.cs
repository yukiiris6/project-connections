using ProjectConnections.Magnetic.Modules;
using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.Pluggable.States
{
    public class PluggablePulling : IState, StateDockedModule
    {
        IContext _context;

        public void Enter(IContext context)
        {
            _context = context;
            if (context is CarriableModule carriableModule)
            {
                carriableModule.CarriableObject.OnCarryChanged += HandleCarryChanged;
                carriableModule.CarriableObject.SetCarryOnTrigger(true);
            }
        }

        public void Exit(IContext context)
        {
            if (context is CarriableModule carriableModule)
            {
                carriableModule.CarriableObject.OnCarryChanged -= HandleCarryChanged;
                carriableModule.CarriableObject.SetCarryOnTrigger(false);
            }
        }

        public void Magnetize(IContext context, Vector2 destination)
        {
            context.Mover.UsePreciseArrival(false);
            context.Mover.MoveTo(destination);
        }

        public void Demagnetize(IContext context)
        {
            context.Mover.Stop();
            context.SetState(new PluggablePulled());
        }

        public void MagnetizeDock(IContext context)
        {
            context.Mover.Stop();
            context.Mover.UsePreciseArrival(true);
            if (context is DockedModule anchorModule)
            {
                context.Mover.MoveTo(anchorModule.OriginalPosition);
            }
            context.SetState(new PluggableReturning());
        }

        public void DemagnetizeDock(IContext context) { }

        void HandleCarryChanged(bool value)
        {
            if (value)
            {
                _context.SetState(new PluggableCarried());
            }
        }
    }
}
