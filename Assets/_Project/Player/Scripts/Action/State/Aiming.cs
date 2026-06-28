using ProjectConnections.Magnetic;
using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.ObjectShared;

namespace ProjectConnections.Player
{
    public class Aiming : ActionState
    {
        ActionContext _context;

        #region Runtime
        public void Enter(ActionContext context)
        {
            _context = context;
            context.CarriableFinder.OnObjectFound += HandleObjectFound;
            context.ActionAnimation.UpdateMagnetizeAnimation(true);
        }

        public void Exit(ActionContext context)
        {
            context.CarriableFinder.OnObjectFound -= HandleObjectFound;
            context.ActionAnimation.UpdateMagnetizeAnimation(false);
        }
        #endregion

        #region Actions
        public void Use(ActionContext context) { }

        public void Interact(ActionContext context) { }

        public void Magnetize(ActionContext context, bool isPressed)
        {
            if (isPressed) return;
            context.MagnetAiming.StopAiming();
            context.SetState(new Resting());
        }
        #endregion

        #region Handlers
        public void HandleObjectFound(CarriableObject carriableObject)
        {
            _context.MagnetAiming.StopAiming();
            _context.Carrier.SetCarryingObject(carriableObject);
            _context.SetState(new Carrying());
        }
        #endregion
    }
}
