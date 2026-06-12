public interface LevelState
{
    void Enter(LevelContext context);
    void Exit(LevelContext context);
    void RestartLevel(LevelContext context);
    void GoToLevel(LevelContext context, string fileName);
    void FinishLevel(LevelContext context);
}