using ProjectConnections.ObjectShared;

namespace ProjectConnections.Player
{
    public interface ActionState
    {
        void Enter(ActionContext context);
        void Exit(ActionContext context);
        void Use(ActionContext context);
        void Interact(ActionContext context);
        void Magnetize(ActionContext context, bool isPressed);
    }
}
