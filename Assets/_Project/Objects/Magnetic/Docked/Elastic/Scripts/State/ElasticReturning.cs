using ProjectConnections.Magnetism.Modules;
using ProjectConnections.Magnetism.States;
using UnityEngine;

namespace ProjectConnections.Magnetism.Elastic.States
{
    public class ElasticReturning : IState
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
            context.Mover.Stop();
            context.Mover.MoveTo(destination);
            context.SetState(new ElasticPulling());
        }

        public void Demagnetize(IContext context) { }

        void OnArrival()
        {
            _context.SetState(new ElasticResting());
        }
    }
}