using ProjectConnections.Magnetic.Modules;
using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.Anchored.States
{
    public class AnchoredPulling : IState, StateAnchorModule
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
            context.Mover.MoveTo(destination);
        }

        public void Demagnetize(IContext context)
        {
            context.Mover.Stop();
            context.SetState(new AnchoredPulled());
        }

        public void MagnetizeAnchor(IContext context)
        {
            context.Mover.Stop();
            if (context is Modules.AnchorModule anchorModule)
            {
                context.Mover.MoveTo(anchorModule.AnchorRange.GetOriginalPosition());
            }
            context.SetState(new AnchoredReturning());
        }

        public void DemagnetizeAnchor(IContext context) { }

        void OnArrival()
        {
            float distance = _context.Mover.GetDistanceTravelled();
            _context.Presenter.PlayStopByDistance(distance);
            _context.SetState(new AnchoredPulled());
        }
    }
}
