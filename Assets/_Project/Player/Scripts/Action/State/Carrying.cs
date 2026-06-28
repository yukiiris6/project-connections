using ProjectConnections.Magnetic;
using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.ObjectShared;

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
            context.ActionAnimation.UpdateCarryingAnimation(true);
        }

        public void Exit(ActionContext context)
        {
            _carryingObject.OnCarryChanged -= HandleCarryChanged;
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
            var requiredObjectType = context.InteractableFinder.GetRequiredObjectType();

            if (requiredObjectType != null)
            {
                if (requiredObjectType == _carryingObject.ObjectType)
                {
                    context.InteractableFinder.Interact();
                    return;
                }
            }

            context.Carrier.Drop();
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
        #endregion
    }
}
