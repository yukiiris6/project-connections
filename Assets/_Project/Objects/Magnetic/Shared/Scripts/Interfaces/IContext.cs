using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetism.States
{
    public interface IContext
    {
        Mover Mover { get; }
        SoundPlayer SoundPlayer { get; }
        Rigidbody2D Rigidbody { get; }
        Presenter Presenter { get; }
        void SetState(IState newState);
    }
}