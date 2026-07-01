using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.ObjectShared;
using System;
using ProjectConnections.Player;
using ProjectConnections.Core;
using ProjectConnections.UIShared;

namespace ProjectConnections.Electric
{
    public class DoorInteractable : MonoBehaviour, IInteractable
    {
        [Header("Interactable Fields")]
        [field: SerializeField, Required] public ObjectType RequiredObjectType { get; private set; }
        [field: SerializeField, Required] public bool IsInteractable { get; private set; } = true;
        [field: SerializeField, Required] public bool ShouldBeGrounded { get; private set; } = false;
        [field: SerializeField, Required] public string InteractionLabel { get; set; } = "Interact";

        [Header("Door References")]
        [SerializeField, Required] ObjectEnergizer electricObject;
        [SerializeField, Required] DoorSoundPlayer soundPlayer;
        [SerializeField, Required] PlayerReferences playerRefs;

        public event Action OnInteractableChanged;

        void OnEnable()
        {
            electricObject.OnChangedState += HandleElectricityChanged;
        }

        void OnDisable()
        {
            electricObject.OnChangedState -= HandleElectricityChanged;
        }

        void HandleElectricityChanged(bool hasEnergy)
        {
            IsInteractable = hasEnergy;
            OnInteractableChanged?.Invoke();
        }

        public void Interact(GameObject gameObject)
        {
            IsInteractable = false;
            playerRefs.InputMapper.ToggleGameplayState(false);
            playerRefs.AnimationBrain.UpdateFinishAnimation();
            playerRefs.Movement.Stop();
            playerRefs.transform.position = transform.position;
            soundPlayer.PlayEnteringSFX();
            CoreSystems.Instance.SceneLoader.FinishLevel();
            OnInteractableChanged?.Invoke();
        }
    }
}
