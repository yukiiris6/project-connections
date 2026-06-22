namespace ProjectConnections.Player
{
    public interface HandsState
    {
        void Interact(HandsContext context);
        void Carry(HandsContext context, CarriableObject carriableObject);
        void Throw(HandsContext context);
    }
}
