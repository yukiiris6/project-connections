using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.States
{
    public class Pulled : IState
    {
        public void Enter(IContext context)
        {
            context.Rigidbody.bodyType = RigidbodyType2D.Kinematic;
        }

        public void Exit(IContext context) { }

        public void Magnetize(IContext context, Vector2 destination) { }

        public void Demagnetize(IContext context)
        {
            context.SetState(new Resting());
        }
    }
}
