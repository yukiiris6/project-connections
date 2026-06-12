using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputMapper : MonoBehaviour
{
    PlayerInput playerInput;

    void GetDependencies()
    {
        if (playerInput != null) return;
        playerInput = FindFirstObjectByType<PlayerInput>();
    }

    public void SetGameplayActionMap()
    {
        playerInput.SwitchCurrentActionMap("Gameplay");
    }

    public void SetUIActionMap()
    {
        playerInput.SwitchCurrentActionMap("UI");
    }
}