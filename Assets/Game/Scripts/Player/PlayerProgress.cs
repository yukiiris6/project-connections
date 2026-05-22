using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerAnimator))]
public class PlayerProgress : MonoBehaviour
{
    [SerializeField] PauseMenuFlow pauseMenuFlow;

    GameManager gameManager;
    LevelManager levelManager;
    IInteractable currentInteractable;

    void Start()
    {
        gameManager = GlobalSystems.Instance.GameManager;
        levelManager = GlobalSystems.Instance.LevelManager;
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
