namespace ProjectConnections.Core
{
    public interface LevelContext
    {
        LevelDataStorage LevelDataStorage { get; }
        SceneLoader SceneLoader { get; }
        SceneMusicPlayer SceneMusicPlayer { get; }
        GameStateSetterBrain GameStateSetter { get; }
        float LoadDelayTime { get; }
        void SetState(SceneState newState);
    }
}
