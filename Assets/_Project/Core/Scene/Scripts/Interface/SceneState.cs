namespace ProjectConnections.Core
{
    public interface SceneState
    {
        void Enter(SceneContext context);
        void Exit(SceneContext context);
        void RestartLevel(SceneContext context);
        void GoToLevel(SceneContext context, string fileName);
        void FinishLevel(SceneContext context);
    }
}
