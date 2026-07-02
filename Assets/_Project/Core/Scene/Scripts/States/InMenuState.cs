using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.Level;

namespace ProjectConnections.Core
{
    public class InMenuState : SceneState
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
            context.SceneLoader.LoadCurrentScene(LevelType.Menu, LevelType.Menu, 0);
            context.SetState(new LoadingState());
        }

        public void GoToLevel(SceneContext context, string sceneName)
        {
            context.LevelDataStorage.ChangeCurrentLevel(sceneName);
            LevelType nextLevelType = context.LevelDataStorage.GetCurrentLevelType();
            context.SceneLoader.LoadCurrentScene(LevelType.Menu, nextLevelType, 0);
            context.SetState(new LoadingState());
        }

        public void FinishLevel(SceneContext context) { }
    }
}
