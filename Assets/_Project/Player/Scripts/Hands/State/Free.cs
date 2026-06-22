using ProjectConnections.Magnetic.Modules;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Player
{
    public class Free : HandsState
    {
        public void Interact(HandsContext context)
        {
            var interactableState = context.InteractableFinder.GetInteractableState();
            if (interactableState is InteractableState state)
            {
                if (state == InteractableState.Free)
                {
                    context.InteractableFinder.Interact();
                }
            }
        }

        public void Carry(HandsContext context, CarriableObject carriableObject)
        {
            switch (carriableObject.ObjectType)
            {
                case ObjectType.Block:
                    break;
                case ObjectType.Plug:
                    context.Carrier.SetCarryingObject(carriableObject);
                    context.MagnetBrain.Release();
                    context.SetState(new CarryingPlug());
                    break;
                default:
                    break;
            }
        }

        public void Throw(HandsContext context) { }
    }
}
