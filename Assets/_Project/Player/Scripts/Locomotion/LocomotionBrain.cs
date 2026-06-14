using UnityEngine;

public class LocomotionBrain : MonoBehaviour, LocomotionContext
{
    [field: SerializeField] public Jumper Jumper { get; private set; }
    [field: SerializeField] public GroundValidator GroundValidator { get; private set; }
    [field: SerializeField] public LocomotionPresenter Presenter { get; private set; }
    [field: SerializeField] public PlayerSoundPlayer SoundPlayer { get; private set; }
    [SerializeField] PlayerController playerController;

    LocomotionState currentState = new Grounded();

    void OnEnable()
    {
        playerController.OnJumpInput += JumpPressed;
    }

    void OnDisable()
    {
        playerController.OnJumpInput -= JumpPressed;
    }

    void JumpPressed(bool wasPressed)
    {
        if (wasPressed) Jump();
        else Release();
    }

    void Jump()
    {
        currentState.Jump(this);
    }

    void Release()
    {
        currentState.Release(this);
    }

    public void SetState(LocomotionState newState)
    {
        currentState.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }
}
