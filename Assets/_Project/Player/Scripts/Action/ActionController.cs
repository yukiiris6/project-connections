using ProjectConnections.ObjectShared;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectConnections.Player
{
    public class ActionController : MonoBehaviour
    {
        [SerializeField, Required] PlayerController playerController;
        [SerializeField, Required] ActionBrain actionBrain;

        void OnEnable()
        {
            playerController.OnUseInput += HandleUse;
            playerController.OnInteractInput += HandleInteract;
            playerController.OnMagnetizeInput += HandleMagnetize;
        }

        void OnDisable()
        {
            playerController.OnUseInput -= HandleUse;
            playerController.OnInteractInput -= HandleInteract;
            playerController.OnMagnetizeInput -= HandleMagnetize;
        }

        void HandleUse(bool isPressed)
        {
            if (isPressed) actionBrain.Use();
        }

        void HandleInteract(bool isPressed)
        {
            if (isPressed) actionBrain.Interact();
        }

        void HandleMagnetize(bool isPressed)
        {
            actionBrain.Magnetize(isPressed);
        }
    }
}
