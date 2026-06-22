using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Player
{
    public interface LocomotionState
    {
        void Enter(LocomotionContext context);
        void Exit(LocomotionContext context);
        void Jump(LocomotionContext context);
        void Release(LocomotionContext context);
    }
}
