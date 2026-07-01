using System;
using ProjectConnections.ObjectShared;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectConnections.Player
{
    public class InteractableController : MonoBehaviour
    {
        [SerializeField, Required] GroundValidator groundValidator;

        public IInteractable SelectedInteractable { get; private set; }
        public bool IsValid { get; private set; }

        public event Action<bool, string> OnUICalled;

        void OnEnable()
        {
            groundValidator.OnExitGround += ValidateInteractable;
            groundValidator.OnLand += ValidateInteractable;
        }

        void OnDisable()
        {
            groundValidator.OnExitGround -= ValidateInteractable;
            groundValidator.OnLand -= ValidateInteractable;
        }

        public void SetInteractable(IInteractable interactable)
        {
            if (SelectedInteractable != null && interactable != SelectedInteractable)
            {
                SelectedInteractable.OnInteractableChanged -= ValidateInteractable;
            }

            SelectedInteractable = interactable;

            if (SelectedInteractable != null)
            {
                SelectedInteractable.OnInteractableChanged += ValidateInteractable;
            }

            ValidateInteractable();
        }

        void ValidateInteractable()
        {
            if (SelectedInteractable == null)
            {
                UpdateInteractableInfo(false);
                return;
            }

            bool IsInteractable = SelectedInteractable.IsInteractable;
            bool groundedIsValid = !SelectedInteractable.ShouldBeGrounded || (SelectedInteractable.ShouldBeGrounded && groundValidator.IsGrounded);
            bool isValid = IsInteractable && groundedIsValid;

            UpdateInteractableInfo(isValid);
        }

        void UpdateInteractableInfo(bool isValid)
        {
            IsValid = isValid;
            OnUICalled?.Invoke(isValid, SelectedInteractable?.InteractionLabel);
        }
    }
}