public interface LevelContext
{
    LevelDataStorage LevelDataStorage { get; }
    SceneLoader SceneLoader { get; }
    SceneMusicPlayer SceneMusicPlayer { get; }
    GameStateSetterBrain GameStateSetter { get; }
    void SetState(SceneState newState);
}