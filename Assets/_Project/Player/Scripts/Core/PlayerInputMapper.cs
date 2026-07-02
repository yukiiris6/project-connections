using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;
using ProjectConnections.Core;

namespace ProjectConnections.UIShared
{
    public class PlayerInputMapper : MonoBehaviour
    {
        [SerializeField, Required] PlayerInput playerInput;

        SceneLoader sceneLoader;

        void GetDependencies()
        {
            if (sceneLoader != null) return;
            sceneLoader = CoreSystems.Instance.SceneLoaderBrain.SceneLoader;
        }

        void OnEnable()
        {
            GetDependencies();
            sceneLoader.OnLevelLoad += HandleLevelLoad;
        }

        void OnDisable()
        {
            sceneLoader.OnLevelLoad -= HandleLevelLoad;
        }

        void HandleLevelLoad()
        {
            ToggleInput(true);
        }

        public void ToggleActionMap()
        {
            if (playerInput.currentActionMap.name.Equals("Gameplay")) SetUIActionMap();
            else SetGameplayActionMap();
        }

        public void SetGameplayActionMap()
        {
            playerInput.SwitchCurrentActionMap("Gameplay");
        }

        public void SetUIActionMap()
        {
            playerInput.SwitchCurrentActionMap("UI");
        }

        public void ToggleInput(bool shouldEnable)
        {
            playerInput.enabled = shouldEnable;
        }
    }
}
