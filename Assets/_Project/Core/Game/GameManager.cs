using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    PlayerInput playerInput;
    bool isPaused = false;
    public bool IsPaused => isPaused;
    bool isOnSlowdown = false;

    void GetDependencies()
    {
        if (playerInput != null) return;
        playerInput = FindFirstObjectByType<PlayerInput>();
    }

    void Start() => GetDependencies();

    public void SlowdownGame()
    {
        Time.timeScale = .25f;
        isOnSlowdown = true;
    }

    public void PauseGame()
    {
        if (isPaused && !isOnSlowdown) return;
        GetDependencies();
        Time.timeScale = 0;
        playerInput.SwitchCurrentActionMap("UI");
        isPaused = true;
        isOnSlowdown = false;
    }

    public void ResumeGame()
    {
        if (!isPaused && !isOnSlowdown) return;
        GetDependencies();
        Time.timeScale = 1;
        playerInput.SwitchCurrentActionMap("Player");
        isPaused = false;
        isOnSlowdown = false;
    }
}
