using ProjectConnections.Magnetic;
using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.ObjectShared;
using System.Collections.Generic;

namespace ProjectConnections.Player
{
    public class Carrying : ActionState
    {
        ActionContext _context;
        CarriableObject _carryingObject;

        #region Runtime
        public void Enter(ActionContext context)
        {
            _context = context;
            _carryingObject = context.Carrier.GetObject();

            _carryingObject.OnCarryChanged += HandleCarryChanged;
            context.InteractableFinder.OnInteractablesChanged += UpdateSelectedInteractable;

            var interactables = context.InteractableFinder.GetInteractables();
            UpdateSelectedInteractable(interactables);
            context.ActionAnimation.UpdateCarryingAnimation(true);
        }

        public void Exit(ActionContext context)
        {
            _carryingObject.OnCarryChanged -= HandleCarryChanged;
            context.InteractableFinder.OnInteractablesChanged -= UpdateSelectedInteractable;
            context.ActionAnimation.UpdateCarryingAnimation(false);
        }
        #endregion

        #region Actions
        public void Use(ActionContext context)
        {
            bool wasThrown = context.Carrier.ThrowObject();
            if (!wasThrown) return;
            context.SetState(new Resting());
        }

        public void Interact(ActionContext context)
        {
            if (!context.InteractableController.IsValid)
            {
                context.Carrier.Drop();
                context.SetState(new Resting());
                return;
            }

            var interactable = context.InteractableController.SelectedInteractable;
            interactable.Interact(_carryingObject.gameObject);
            context.SetState(new Resting());
        }

        public void Magnetize(ActionContext context, bool isPressed) { }
        #endregion

        #region Handlers
        void HandleCarryChanged(bool value)
        {
            if (value) return;
            _context.SetState(new Resting());
        }

        public void UpdateSelectedInteractable(List<IInteractable> interactables)
        {
            IInteractable selectedInteractable = null;

            foreach (var interactable in interactables)
            {
                if (interactable.RequiredObjectType != _carryingObject.ObjectType) continue;
                selectedInteractable = interactable;
            }

            _context.InteractableController.SetInteractable(selectedInteractable);
        }
        #endregion
    }
}
