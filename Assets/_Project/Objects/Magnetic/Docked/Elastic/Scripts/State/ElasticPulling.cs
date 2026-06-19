using ProjectConnections.Magnetic.Modules;
using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.Elastic.States
{
    public class ElasticPulling : IState
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
            if (context is DockedModule anchorModule)
            {
                context.Mover.UsePreciseArrival(true);
                context.Mover.MoveTo(anchorModule.OriginalPosition);
            }
            context.SetState(new ElasticReturning());
        }

        void OnArrival()
        {
            _context.SetState(new ElasticPulled());
        }
    }
}