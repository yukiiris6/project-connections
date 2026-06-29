using ProjectConnections.ObjectShared;
using Sirenix.OdinInspector;
using UnityEngine;
using ProjectConnections.Magnetic;
using System;

namespace ProjectConnections.Electric
{
    public class SocketInteractable : MonoBehaviour, IInteractable
    {
        [Header("Interactable Fields")]
        [field: SerializeField, Required] public ObjectType RequiredObjectType { get; private set; }
        [field: SerializeField, Required] public bool IsInteractable { get; private set; } = true;
        [field: SerializeField, Required] public bool ShouldBeGrounded { get; private set; } = false;
        [field: SerializeField, Required] public string InteractionLabel { get; set; } = "Interact";

        [Header("References")]
        [SerializeField, Required] SocketConnector socketConnector;
        [SerializeField, Required] ObjectSoundPlayer soundPlayer;

        public event Action OnInteractableChanged;

        void OnEnable() => socketConnector.OnDisconnect += HandleDisconnect;
        void OnDisable() => socketConnector.OnDisconnect -= HandleDisconnect;

        void HandleDisconnect()
        {
            IsInteractable = true;
            OnInteractableChanged?.Invoke();
        }

        public void Interact(GameObject objectToInteract)
        {
            var electricityProvider = objectToInteract.GetComponent<ElectricityProvider>();
            if (electricityProvider == null) return;

            var plugReferences = objectToInteract.GetComponent<PlugReferences>();
            if (plugReferences == null) return;

            var carriableObject = objectToInteract.GetComponent<CarriableObject>();
            if (carriableObject == null) return;

            carriableObject.LetGo();
            plugReferences.Mover.SnapTo(socketConnector.ConnectionAnchor.position);
            plugReferences.Rotator.SetCalculatedRotation(socketConnector.ConnectionRotation);
            socketConnector.ConnectPlug(electricityProvider);
            soundPlayer.PlayConnectionSFX();

            IsInteractable = false;
            OnInteractableChanged?.Invoke();
        }
    }
}
