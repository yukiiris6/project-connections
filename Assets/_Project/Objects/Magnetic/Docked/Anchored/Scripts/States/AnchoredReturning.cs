using ProjectConnections.Magnetism.Modules;
using ProjectConnections.Magnetism.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetism.Anchored.States
{
    public class AnchoredReturning : IState, StateDockedModule
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

        public void MagnetizeDock(IContext context)
        {
            if (context is DockedModule anchorModule)
            {
                context.Mover.MoveTo(anchorModule.OriginalPosition);
            }
        }

        public void DemagnetizeDock(IContext context)
        {
            context.Mover.Stop();
            context.SetState(new AnchoredPulled());
        }

        void OnArrival()
        {
            _context.SetState(new AnchoredResting());
        }
    }
}
