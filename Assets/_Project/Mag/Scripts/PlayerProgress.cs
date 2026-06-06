using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerJumping))]
public class PlayerProgress : MonoBehaviour
{
    PauseMenuFlow pauseMenuFlow;
    GameManager gameManager;
    LevelManager levelManager;
    IInteractable currentInteractable;
    PlayerMovement playerMovement;
    PlayerJumping playerJumping;

    float lastPauseTime = 0;
    float pauseCooldown = 0.05f;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        playerJumping = GetComponent<PlayerJumping>();
    }

    void Start()
    {
        gameManager = GlobalSystems.Instance.GameManager;
        levelManager = GlobalSystems.Instance.LevelManager;
        pauseMenuFlow = OverlayCanvas.Instance.PauseMenuFlow;
    }

    public void SetInteractable(IInteractable interactable)
    {
        currentInteractable = interactable;
    }

    void OnInteract(InputValue value)
    {
        if (levelManager.IsLoading) return;
        if (!playerJumping.IsGrounded) return;
        if (value.isPressed)
        {
            if (currentInteractable != null)
            {
                currentInteractable.Interact(gameObject);
                playerMovement.StopAllVelocity();
            }
        }
    }

    void OnPause(InputValue value)
    {
        if (levelManager.IsLoading) return;

        if (value.isPressed && Time.unscaledTime > lastPauseTime + pauseCooldown)
        {
            lastPauseTime = Time.unscaledTime;
            if (gameManager.IsPaused)
            {
                gameManager.ResumeGame();
                pauseMenuFlow.OnClickResume();
            }
            else
            {
                gameManager.PauseGame();
                pauseMenuFlow.OpenMenu();
            }
        }
    }
}
