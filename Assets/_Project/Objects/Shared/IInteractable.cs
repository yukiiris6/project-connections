using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.ObjectShared
{
    public interface IInteractable
    {
        ObjectType? RequiredObjectType { get; }
        void Interact();
    }
}
