using UnityEngine;

public class InMenuState : LevelState
{
    public void Enter(LevelContext context)
    {
        AudioClip currentLevelMusic = context.LevelDataStorage.GetCurrentLevelMusic();
        context.SceneMusicPlayer.SetMusicAndPlay(currentLevelMusic);
        context.GameBrain.ResumeGame();
    }

    public void Exit(LevelContext context) { }

    public void RestartLevel(LevelContext context)
    {
        context.SceneLoader.LoadCurrentScene(true, true);
        context.SetState(new LoadingState());
    }

    public void GoToLevel(LevelContext context, string fileName)
    {
        context.LevelDataStorage.ChangeCurrentLevel(fileName);
        bool isToMenu = context.LevelDataStorage.CurrentLevelIsMenu();
        context.SceneLoader.LoadCurrentScene(true, isToMenu);
        context.SetState(new LoadingState());
    }

    public void FinishLevel(LevelContext context) { }

}