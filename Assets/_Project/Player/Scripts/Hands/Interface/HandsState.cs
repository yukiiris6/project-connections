using ProjectConnections.ObjectShared;

namespace ProjectConnections.Player
{
    public interface HandsState
    {
        void Enter(HandsContext context);
        void Exit(HandsContext context);
        void Interact(HandsContext context);
        void Carry(HandsContext context, CarriableObject carriableObject);
        void Throw(HandsContext context);
    }
}
