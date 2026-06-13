public interface LevelContext
{
    LevelDataStorage LevelDataStorage { get; }
    SceneLoader SceneLoader { get; }
    SceneMusicPlayer SceneMusicPlayer { get; }
    GameBrain GameBrain { get; }
    void SetState(LevelState newState);
}