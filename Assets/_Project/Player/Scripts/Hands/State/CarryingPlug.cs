using ProjectConnections.Magnetic.Modules;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Player
{
    public class CarryingPlug : HandsState
    {
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
    }
}
