using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Shared
{
    public interface IInteractable
    {
        InteractableState State { get; }
        void Interact();
    }
}
