using UnityEngine;

public class MagnetBrain : MonoBehaviour, MagnetContext
{
    [field: SerializeField] public MagnetAiming MagnetAiming { get; private set; }
    [field: SerializeField] public MagnetPresenter MagnetPresenter { get; private set; }
    [SerializeField] PlayerController playerController;

    MagnetState currentState = new Standby();

    void OnEnable()
    {
        playerController.OnAimInput += AimPressed;
    }

    void OnDisable()
    {
        playerController.OnAimInput -= AimPressed;
    }

    void AimPressed(bool wasPressed)
    {
        if (wasPressed) Aim();
        else Release();
    }

    void Aim()
    {
        currentState.Aim(this);
    }

    void Release()
    {
        currentState.Release(this);
    }

    public void SetState(MagnetState newState)
    {
        currentState = newState;
    }
}