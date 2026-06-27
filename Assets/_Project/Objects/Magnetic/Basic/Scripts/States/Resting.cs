using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.Magnetic.Modules;

namespace ProjectConnections.Magnetic.States
{
    public class Resting : IState
    {
        public void Enter(IContext context)
        {
            if (context is not CarriableModule carriableModule) return;
            context.Rigidbody.bodyType = RigidbodyType2D.Dynamic;
            carriableModule.CarriableObject.SetCarryOnTrigger(false);
        }

        public void Exit(IContext context) { }

        public void Magnetize(IContext context, Vector2 destination)
        {
            Vector2 constrainedDestination = context.Constrainer.ConstrainStopDistance(destination);
            context.Mover.MoveTo(constrainedDestination);
            context.SetState(new Pulling());
        }

        public void Demagnetize(IContext context) { }
    }
}
