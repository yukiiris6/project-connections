using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] PlayerInteraction interaction;

    public event Action<Vector2> OnMoveInput;
    public event Action<bool> OnJumpInput;
    public event Action<bool> OnInteractInput;
    public event Action<bool> OnAimInput;
    public event Action<bool> OnPauseInput;

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

    void OnAim(InputValue value)
    {
        OnAimInput?.Invoke(value.isPressed);
    }

    void OnInteract(InputValue value)
    {
        OnInteractInput?.Invoke(value.isPressed);
    }

    void OnPause(InputValue value)
    {
        if (value.isPressed && lastPauseTime > pauseCooldown)
        {
            lastPauseTime = 0;
            OnPauseInput?.Invoke(value.isPressed);
        }
    }
}