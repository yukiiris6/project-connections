using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.States
{
    public class Pulling : IState
    {
        IContext _context;

        public void Enter(IContext context)
        {
            _context = context;
            context.Mover.OnDestinationReached += OnArrival;
            context.Rigidbody.bodyType = RigidbodyType2D.Kinematic;
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
            context.SetState(new Resting());
        }

        void OnArrival(float distance)
        {
            _context.Presenter.PlayStopByDistance(distance);
            _context.SetState(new Pulled());
        }
    }
}
