using UnityEngine;

public class LoadingState : LevelState
{
    LevelContext _context;

    public void Enter(LevelContext context)
    {
        _context = context;
        context.SceneLoader.OnLevelLoad += OnLoad;
    }

    public void Exit(LevelContext context)
    {
        context.SceneLoader.OnLevelLoad -= OnLoad;
    }

    public void GoToLevel(LevelContext context, string fileName) { }
    public void FinishLevel(LevelContext context) { }
    public void RestartLevel(LevelContext context) { }

    void OnLoad()
    {
        bool isToMenu = _context.LevelDataStorage.CurrentLevelIsMenu();
        if (isToMenu) _context.SetState(new InMenuState());
        else _context.SetState(new InLevelState());
    }
}