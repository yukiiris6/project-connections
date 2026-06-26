using ProjectConnections.Magnetic.Modules;
using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.Anchored.States
{
    public class AnchoredResting : IState, StateAnchorModule
    {
        public void Enter(IContext context)
        {
            if (context is Modules.AnchorModule anchorModule)
            {
                context.Mover.SnapTo(anchorModule.AnchorRange.GetOriginalPosition());
            }
            context.Presenter.PlayStopEffects();
        }

        public void Magnetize(IContext context, Vector2 destination)
        {
            context.Mover.MoveTo(destination);
            context.SetState(new AnchoredPulling());
        }

        public void Exit(IContext context) { }
        public void Demagnetize(IContext context) { }
        public void MagnetizeAnchor(IContext context) { }
        public void DemagnetizeAnchor(IContext context) { }
    }
}
