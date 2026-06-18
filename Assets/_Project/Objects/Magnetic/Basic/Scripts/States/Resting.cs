using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetism.States
{
    public class Resting : IState
    {
        public void Enter(IContext context)
        {
            context.Rigidbody.bodyType = RigidbodyType2D.Dynamic;
        }

        public void Exit(IContext context) { }

        public void Magnetize(IContext context, Vector2 destination)
        {
            context.Mover.MoveTo(destination);
            context.SetState(new Pulling());
        }

        public void Demagnetize(IContext context) { }
    }
}