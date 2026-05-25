using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerProgress : MonoBehaviour
{
    PauseMenuFlow pauseMenuFlow;
    GameManager gameManager;
    LevelManager levelManager;
    IInteractable currentInteractable;
    PlayerMovement playerMovement;

    void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
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
        if (value.isPressed)
        {
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
