using ProjectConnections.Magnetic;
using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.ObjectShared;
using System.Collections.Generic;

namespace ProjectConnections.Player
{
    public class Resting : ActionState
    {
        ActionContext _context;

        #region Runtime
        public void Enter(ActionContext context)
        {
            _context = context;
            context.CarriableFinder.OnObjectFound += HandleObjectFound;
            context.InteractableFinder.OnInteractablesChanged += UpdateSelectedInteractable;
            var interactables = context.InteractableFinder.GetInteractables();
            UpdateSelectedInteractable(interactables);
        }

        public void Exit(ActionContext context)
        {
            context.CarriableFinder.OnObjectFound -= HandleObjectFound;
            context.InteractableFinder.OnInteractablesChanged -= UpdateSelectedInteractable;
        }
        #endregion

        #region Actions
        public void Use(ActionContext context) { }

        public void Interact(ActionContext context)
        {
            if (!context.InteractableController.IsValid) return;

            var interactable = context.InteractableController.SelectedInteractable;
            interactable.Interact(context.Carrier.gameObject);
        }

        public void Magnetize(ActionContext context, bool isPressed)
        {
            if (!isPressed) return;
            context.MagnetAiming.Aim();
            context.SetState(new Aiming());
        }
        #endregion

        #region Handlers
        public void HandleObjectFound(CarriableObject carriableObject)
        {
            _context.MagnetAiming.StopAiming();
            _context.Carrier.SetCarryingObject(carriableObject);
            _context.SetState(new Carrying());
        }

        public void UpdateSelectedInteractable(List<IInteractable> interactables)
        {
            IInteractable selectedInteractable = null;

            foreach (var interactable in interactables)
            {
                if (interactable.RequiredObjectType != ObjectType.Player) continue;
                selectedInteractable = interactable;
            }

            _context.InteractableController.SetInteractable(selectedInteractable);
        }
        #endregion
    }
}
