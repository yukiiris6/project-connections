using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.SceneUI;

namespace ProjectConnections.Player
{
    public interface ActionContext
    {
        Carrier Carrier { get; }
        CarriableFinder CarriableFinder { get; }
        InteractableFinder InteractableFinder { get; }
        InteractableController InteractableController { get; }
        MagnetAiming MagnetAiming { get; }
        ActionAnimation ActionAnimation { get; }
        void SetState(ActionState newState);
    }
}
