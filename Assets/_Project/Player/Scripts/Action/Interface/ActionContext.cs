using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Player
{
    public interface ActionContext
    {
        Carrier Carrier { get; }
        CarriableFinder CarriableFinder { get; }
        InteractableFinder InteractableFinder { get; }
        MagnetAiming MagnetAiming { get; }
        ActionAnimation ActionAnimation { get; }
        void SetState(ActionState newState);
    }
}
