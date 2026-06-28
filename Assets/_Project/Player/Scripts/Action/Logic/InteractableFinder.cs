using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.Shared;
using ProjectConnections.ObjectShared;

namespace ProjectConnections.Player
{
    public class InteractableFinder : MonoBehaviour
    {
        [SerializeField, Required] PlayerMovement playerMovement;
        [SerializeField, Required] GroundValidator groundValidator;
        [SerializeField, Required] string interactableTagString = "Interactable";

        IInteractable currentInteractable;

        public void Interact()
        {
            if (!groundValidator.IsGrounded) return;
            if (currentInteractable != null)
            {
                currentInteractable.Interact();
                playerMovement.Stop();
            }
        }

        public ObjectType? GetRequiredObjectType()
        {
            return currentInteractable?.RequiredObjectType;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(interactableTagString))
            {
                var interactable = other.GetComponent<IInteractable>();
                if (interactable != null) currentInteractable = interactable;
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(interactableTagString))
            {
                currentInteractable = null;
            }
        }
    }
}
