using ProjectConnections.Magnetic.Modules;
using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.Elastic.States
{
    public class ElasticResting : IState
    {
        public void Enter(IContext context)
        {
            if (context is AnchorModule anchorModule)
            {
                context.Mover.SnapTo(anchorModule.AnchorRange.GetOriginalPosition());
            }
            context.Presenter.PlayStopEffects();
        }

        public void Magnetize(IContext context, Vector2 destination)
        {
            context.Mover.MoveTo(destination);
            context.SetState(new ElasticPulling());
        }

        public void Demagnetize(IContext context) { }
        public void Exit(IContext context) { }
    }
}
