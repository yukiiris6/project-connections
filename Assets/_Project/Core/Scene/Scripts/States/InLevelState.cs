using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.Level;

namespace ProjectConnections.Core
{
    public class InLevelState : SceneState
    {
        public void Enter(SceneContext context)
        {
            AudioClip currentLevelMusic = context.LevelDataStorage.GetCurrentLevelMusic();
            context.MusicPlayer.SetAudioClip(currentLevelMusic);
            context.MusicPlayer.PlayMusic();
        }

        public void Exit(SceneContext context) { }

        public void RestartLevel(SceneContext context)
        {
            context.SceneLoader.LoadCurrentScene(LevelType.Stage, LevelType.Stage, 0);
            context.SetState(new LoadingState());
        }

        public void GoToLevel(SceneContext context, string sceneName)
        {
            context.LevelDataStorage.ChangeCurrentLevel(sceneName);
            LevelType nextLevelType = context.LevelDataStorage.GetCurrentLevelType();
            context.SceneLoader.LoadCurrentScene(LevelType.Stage, nextLevelType, 0);
            context.SetState(new LoadingState());
        }

        public void FinishLevel(SceneContext context)
        {
            context.LevelDataStorage.FinishCurrentLevel();
            context.LevelDataStorage.ChangeToNextLevel();
            LevelType nextLevelType = context.LevelDataStorage.GetCurrentLevelType();
            context.SceneLoader.LoadCurrentScene(LevelType.Stage, nextLevelType, context.LoadDelayTime);
            context.SetState(new LoadingState());
        }
    }
}
