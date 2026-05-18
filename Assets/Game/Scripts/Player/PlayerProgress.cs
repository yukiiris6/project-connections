using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerProgress : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;

    PlayerAnimator playerAnimator;
    DoorState doorState;
    public bool HasFinished { get; private set; }

    void Awake()
    {
        playerAnimator = GetComponent<PlayerAnimator>();
    }

    void Start()
    {
        doorState = FindFirstObjectByType<DoorState>();
    }

    void OnActivateDoor(InputValue value)
    {
        levelManager.RestartLevel();
        if (HasFinished) return;
        if (value.isPressed && doorState.isDoorActive && doorState.isInsideDoor)
        {
            transform.position = doorState.transform.position;
            playerAnimator.PlayFinishAnimation();
            HasFinished = true;
        }
    }
}
