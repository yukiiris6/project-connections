public interface LevelContext
{
    LevelDataStorage LevelDataStorage { get; }
    SceneLoader SceneLoader { get; }
    SceneMusicPlayer SceneMusicPlayer { get; }
    void SetState(LevelState newState);
}