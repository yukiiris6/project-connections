using ProjectConnections.Magnetic.Modules;
using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.ObjectShared;

namespace ProjectConnections.Player
{
    public class CarryingPlug : HandsState
    {
        HandsContext _context;
        CarriableObject _carryingObject;

        public void Enter(HandsContext context)
        {
            _context = context;
            _carryingObject = context.Carrier.GetObject();
            _carryingObject.OnCarryChanged += HandleCarryChanged;
        }

        public void Exit(HandsContext context)
        {
            _carryingObject.OnCarryChanged -= HandleCarryChanged;
        }

        public void Interact(HandsContext context)
        {
            var interactableState = context.InteractableFinder.GetInteractableState();
            if (interactableState is InteractableState state)
            {
                if (state == InteractableState.CarryingPlug)
                {
                    context.InteractableFinder.Interact();
                    return;
                }
            }

            context.Carrier.Drop();
            context.SetState(new Free());
        }

        public void Carry(HandsContext context, CarriableObject carriableObject) { }

        public void Throw(HandsContext context)
        {
            bool wasThrown = context.Carrier.ThrowObject();
            if (wasThrown) context.SetState(new Free());
        }

        void HandleCarryChanged(bool value)
        {
            if (!value) _context.SetState(new Free());
        }
    }
}
