using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Player
{
    public interface HandsContext
    {
        Carrier Carrier { get; }
        CarriableFinder CarriableFinder { get; }
        InteractableFinder InteractableFinder { get; }
        MagnetBrain MagnetBrain { get; }
        void SetState(HandsState newState);
    }
}
