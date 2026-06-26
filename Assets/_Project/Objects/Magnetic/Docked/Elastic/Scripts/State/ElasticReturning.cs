using ProjectConnections.Magnetic.Modules;
using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.Elastic.States
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

        void OnArrival(float distance)
        {
            _context.SetState(new ElasticResting());
        }

        public void Demagnetize(IContext context) { }
    }
}
