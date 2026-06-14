using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] GroundValidator groundValidator;
    [SerializeField] string interactableTagString = "Interactable";

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
