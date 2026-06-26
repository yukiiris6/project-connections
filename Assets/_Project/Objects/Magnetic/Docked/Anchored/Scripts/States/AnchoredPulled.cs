using ProjectConnections.Magnetic.Modules;
using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.Anchored.States
{
    public class AnchoredPulled : IState, StateAnchorModule
    {
        public void Magnetize(IContext context, Vector2 destination)
        {
            bool isSameAsCurrentPosition = context.Mover.IsSameAsCurrentPosition(destination);
            if (!isSameAsCurrentPosition)
            {
                context.Mover.MoveTo(destination);
                context.SetState(new AnchoredPulling());
            }
        }

        public void MagnetizeAnchor(IContext context)
        {
            if (context is Modules.AnchorModule anchorModule)
            {
                context.Mover.MoveTo(anchorModule.AnchorRange.GetOriginalPosition());
            }
            context.SetState(new AnchoredReturning());
        }

        public void DemagnetizeAnchor(IContext context) { }

        public void Demagnetize(IContext context) { }
        public void Enter(IContext context) { }
        public void Exit(IContext context) { }
    }
}
