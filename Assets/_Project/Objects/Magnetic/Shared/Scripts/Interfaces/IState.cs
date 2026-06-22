using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.States
{
    public interface IState
    {
        void Enter(IContext context);
        void Exit(IContext context);
        void Magnetize(IContext context, Vector2 destination);
        void Demagnetize(IContext context);
    }
}
