using UnityEngine;
using Sirenix.OdinInspector;

public class InLevelState : SceneState
{
    public void Enter(LevelContext context)
    {
        AudioClip currentLevelMusic = context.LevelDataStorage.GetCurrentLevelMusic();
        context.SceneMusicPlayer.SetMusicAndPlay(currentLevelMusic);
        context.GameStateSetter.ResumeGame();
    }

    public void Exit(LevelContext context) { }

    public void RestartLevel(LevelContext context)
    {
        context.SceneLoader.LoadCurrentScene(LevelType.Stage, LevelType.Stage);
        context.SetState(new LoadingState());
    }

    public void GoToLevel(LevelContext context, string fileName)
    {
        context.LevelDataStorage.ChangeCurrentLevel(fileName);
        LevelType nextLevelType = context.LevelDataStorage.GetCurrentLevelType();
        context.SceneLoader.LoadCurrentScene(LevelType.Stage, nextLevelType);
        context.SetState(new LoadingState());
    }

    public void FinishLevel(LevelContext context)
    {
        context.LevelDataStorage.FinishCurrentLevel();
        context.LevelDataStorage.ChangeToNextLevel();
        LevelType nextLevelType = context.LevelDataStorage.GetCurrentLevelType();
        context.SceneLoader.LoadCurrentScene(LevelType.Stage, nextLevelType);
        context.SetState(new LoadingState());
    }
}