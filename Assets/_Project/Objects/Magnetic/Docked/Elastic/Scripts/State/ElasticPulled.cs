using ProjectConnections.Magnetic.Modules;
using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.Elastic.States
{
    public class ElasticPulled : IState
    {
        public void Magnetize(IContext context, Vector2 destination)
        {
            bool isSameAsCurrentPosition = context.Mover.IsSameAsCurrentPosition(destination);
            if (!isSameAsCurrentPosition)
            {
                context.Mover.MoveTo(destination);
                context.SetState(new ElasticPulling());
            }
        }

        public void Demagnetize(IContext context)
        {
            if (context is AnchorModule anchorModule)
            {
                context.Mover.MoveTo(anchorModule.AnchorRange.GetOriginalPosition());
            }
            context.SetState(new ElasticReturning());
        }

        public void Enter(IContext context) { }
        public void Exit(IContext context) { }
    }
}
