using UnityEngine;
using Sirenix.OdinInspector;
using System;

namespace ProjectConnections.ObjectShared
{
    public interface IInteractable
    {
        ObjectType RequiredObjectType { get; }
        string InteractionLabel { get; }
        bool ShouldBeGrounded { get; }
        bool IsInteractable { get; }
        event Action OnInteractableChanged;
        void Interact(GameObject gameObject);
    }
}
