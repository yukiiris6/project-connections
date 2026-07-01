using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.Level;

namespace ProjectConnections.Core
{
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
            context.SceneLoader.LoadCurrentScene(LevelType.Stage, LevelType.Stage, 0);
            context.SetState(new LoadingState());
        }

        public void GoToLevel(LevelContext context, string sceneName)
        {
            context.LevelDataStorage.ChangeCurrentLevel(sceneName);
            LevelType nextLevelType = context.LevelDataStorage.GetCurrentLevelType();
            context.SceneLoader.LoadCurrentScene(LevelType.Stage, nextLevelType, 0);
            context.SetState(new LoadingState());
        }

        public void FinishLevel(LevelContext context)
        {
            context.LevelDataStorage.FinishCurrentLevel();
            context.LevelDataStorage.ChangeToNextLevel();
            LevelType nextLevelType = context.LevelDataStorage.GetCurrentLevelType();
            context.SceneLoader.LoadCurrentScene(LevelType.Stage, nextLevelType, context.LoadDelayTime);
            context.SetState(new LoadingState());
        }
    }
}
