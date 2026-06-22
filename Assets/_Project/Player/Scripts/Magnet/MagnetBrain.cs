using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Player
{
    public class MagnetBrain : MonoBehaviour, MagnetContext
    {
        [field: SerializeField] public MagnetAiming MagnetAiming { get; private set; }
        [field: SerializeField] public MagnetPresenter MagnetPresenter { get; private set; }
        [SerializeField, Required] PlayerController playerController;

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

        public void Aim()
        {
            currentState.Aim(this);
        }

        public void Release()
        {
            currentState.Release(this);
        }

        public void SetState(MagnetState newState)
        {
            currentState = newState;
        }
    }
}
