using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.Level;

namespace ProjectConnections.Core
{
    public class LoadingState : SceneState
    {
        SceneContext _context;

        public void Enter(SceneContext context)
        {
            _context = context;
            context.SceneLoader.OnLevelLoad += OnLoad;
        }

        public void Exit(SceneContext context)
        {
            context.SceneLoader.OnLevelLoad -= OnLoad;
        }

        public void GoToLevel(SceneContext context, string fileName) { }
        public void FinishLevel(SceneContext context) { }
        public void RestartLevel(SceneContext context) { }

        void OnLoad()
        {
            bool isToMenu = _context.LevelDataStorage.GetCurrentLevelType() == LevelType.Menu;
            if (isToMenu) _context.SetState(new InMenuState());
            else _context.SetState(new InLevelState());
        }
    }
}
