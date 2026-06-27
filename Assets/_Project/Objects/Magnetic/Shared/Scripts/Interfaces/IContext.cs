using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.States
{
    public interface IContext
    {
        Mover Mover { get; }
        Rotator Rotator { get; }
        Constrainer Constrainer { get; }
        Rigidbody2D Rigidbody { get; }
        Presenter Presenter { get; }
        void SetState(IState newState);
    }
}
