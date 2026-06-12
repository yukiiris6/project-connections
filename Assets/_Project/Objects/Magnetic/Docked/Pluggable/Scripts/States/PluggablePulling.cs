using ProjectConnections.Magnetism.Modules;
using ProjectConnections.Magnetism.States;
using UnityEngine;

namespace ProjectConnections.Magnetism.Pluggable.States
{
    public class PluggablePulling : IState, StateDockedModule
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

        void OnArrival()
        {
            _context.SetState(new PluggablePulled());
        }
    }
}
