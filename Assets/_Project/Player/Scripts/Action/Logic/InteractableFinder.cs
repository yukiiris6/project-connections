using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.Shared;
using ProjectConnections.ObjectShared;
using System.Collections.Generic;
using System;

namespace ProjectConnections.Player
{
    public class InteractableFinder : MonoBehaviour
    {
        [SerializeField, Required] PlayerMovement playerMovement;
        [SerializeField, Required] string interactableTagString = "Interactable";

        public event Action<List<IInteractable>> OnInteractablesChanged;

        [ShowInInspector, ReadOnly]
        List<IInteractable> interactablesFound = new();

        public List<IInteractable> GetInteractables()
        {
            return interactablesFound;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(interactableTagString))
            {
                var interactable = other.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactablesFound.Add(interactable);
                    OnInteractablesChanged?.Invoke(interactablesFound);
                }
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag(interactableTagString))
            {
                var interactable = other.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    interactablesFound.Remove(interactable);
                    OnInteractablesChanged?.Invoke(interactablesFound);
                }
            }
        }
    }
}
