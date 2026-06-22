namespace ProjectConnections.Player
{
    public interface MagnetContext
    {
        MagnetAiming MagnetAiming { get; }
        void SetState(MagnetState newState);
    }
}
