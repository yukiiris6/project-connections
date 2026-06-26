using ProjectConnections.Magnetic.Modules;
using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.Anchored.States
{
    public class AnchoredReturning : IState, StateAnchorModule
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

        public void MagnetizeAnchor(IContext context)
        {
            if (context is Modules.AnchorModule anchorModule)
            {
                context.Mover.MoveTo(anchorModule.AnchorRange.GetOriginalPosition());
            }
        }

        public void DemagnetizeAnchor(IContext context)
        {
            context.Mover.Stop();
            context.SetState(new AnchoredPulled());
        }

        void OnArrival(float distance)
        {
            _context.SetState(new AnchoredResting());
        }

        public void Magnetize(IContext context, Vector2 destination) { }
        public void Demagnetize(IContext context) { }
    }
}
