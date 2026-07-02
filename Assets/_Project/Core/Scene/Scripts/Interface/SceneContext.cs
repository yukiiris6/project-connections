namespace ProjectConnections.Core
{
    public interface SceneContext
    {
        LevelDataStorage LevelDataStorage { get; }
        SceneLoader SceneLoader { get; }
        MusicPlayer MusicPlayer { get; }
        float LoadDelayTime { get; }
        void SetState(SceneState newState);
    }
}
