using ProjectConnections.Magnetic;
using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.ObjectShared;

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
        }

        public void Exit(ActionContext context)
        {
            context.CarriableFinder.OnObjectFound -= HandleObjectFound;
        }
        #endregion

        #region Actions
        public void Use(ActionContext context) { }

        public void Interact(ActionContext context)
        {
            var requiredObjectType = context.InteractableFinder.GetRequiredObjectType();
            if (requiredObjectType != null) return;
            context.InteractableFinder.Interact();
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
            switch (carriableObject.ObjectType)
            {
                case ObjectType.Block:
                    break;
                case ObjectType.Plug:
                    _context.Carrier.SetCarryingObject(carriableObject);
                    _context.SetState(new Carrying());
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
