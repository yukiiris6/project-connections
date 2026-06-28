using System;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;
using ProjectConnections.SceneUI;

namespace ProjectConnections.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField, Required] PauseMenuPresenter pauseMenuPresenter;
        [SerializeField] ActionBrain handsBrain;

        public event Action<Vector2> OnMoveInput;
        public event Action<bool> OnJumpInput;
        public event Action<bool> OnInteractInput;
        public event Action<bool> OnMagnetizeInput;
        public event Action<bool> OnPauseInput;
        public event Action<bool> OnUseInput;

        float pauseCooldown = 0.05f;
        float lastPauseTime = 0;

        void Update()
        {
            lastPauseTime += Time.deltaTime;
        }

        void OnMove(InputValue value)
        {
            OnMoveInput?.Invoke(value.Get<Vector2>());
        }

        void OnJump(InputValue value)
        {
            OnJumpInput?.Invoke(value.isPressed);
        }

        void OnMagnetize(InputValue value)
        {
            OnMagnetizeInput?.Invoke(value.isPressed);
        }

        void OnInteract(InputValue value)
        {
            OnInteractInput?.Invoke(value.isPressed);
        }

        void OnUse(InputValue value)
        {
            OnUseInput?.Invoke(value.isPressed);
        }

        void OnPause(InputValue value)
        {
            if (value.isPressed && lastPauseTime > pauseCooldown)
            {
                lastPauseTime = 0;
                pauseMenuPresenter.ToggleMenu();
                OnPauseInput?.Invoke(value.isPressed);
            }
        }
    }
}
