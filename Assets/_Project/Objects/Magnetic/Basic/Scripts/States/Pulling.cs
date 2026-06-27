using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.Magnetic.Modules;

namespace ProjectConnections.Magnetic.States
{
    public class Pulling : IState
    {
        IContext _context;

        public void Enter(IContext context)
        {
            if (context is not CarriableModule carriableModule) return;
            _context = context;
            context.Mover.OnDestinationReached += OnArrival;
            context.Rigidbody.bodyType = RigidbodyType2D.Kinematic;
            carriableModule.CarriableObject.OnCarryChanged += HandleCarryChanged;
            carriableModule.CarriableObject.SetCarryOnTrigger(true);
        }

        public void Exit(IContext context)
        {
            if (context is not CarriableModule carriableModule) return;
            context.Mover.OnDestinationReached -= OnArrival;
            carriableModule.CarriableObject.OnCarryChanged += HandleCarryChanged;
        }

        public void Magnetize(IContext context, Vector2 destination)
        {
            Vector2 constrainedDestination = context.Constrainer.ConstrainStopDistance(destination);
            context.Mover.MoveTo(constrainedDestination);
        }

        public void Demagnetize(IContext context)
        {
            context.Mover.Stop();
            context.SetState(new Resting());
        }

        void OnArrival()
        {
            float distance = _context.Mover.GetDistanceTravelled();
            _context.Presenter.PlayStopByDistance(distance);
            _context.SetState(new Resting());
        }

        void HandleCarryChanged(bool value)
        {
            if (value) _context.SetState(new Carried());
        }
    }
}
