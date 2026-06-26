using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.ObjectShared;

namespace ProjectConnections.Player
{
    public class HandsBrain : MonoBehaviour, HandsContext
    {
        [SerializeField] PlayerController playerController;

        [field: SerializeField, Required] public Carrier Carrier { get; private set; }
        [field: SerializeField, Required] public CarriableFinder CarriableFinder { get; private set; }
        [field: SerializeField, Required] public InteractableFinder InteractableFinder { get; private set; }
        [field: SerializeField, Required] public MagnetBrain MagnetBrain { get; private set; }

        HandsState currentState = new Free();

        void OnEnable()
        {
            playerController.OnThrowInput += HandleThrow;
            playerController.OnInteractInput += HandleInteract;
            CarriableFinder.OnObjectFound += Carry;
        }

        void OnDisable()
        {
            playerController.OnThrowInput -= HandleThrow;
            playerController.OnInteractInput -= HandleInteract;
            CarriableFinder.OnObjectFound -= Carry;
        }

        void HandleThrow(bool isPressed)
        {
            if (isPressed) Throw();
        }

        void HandleInteract(bool isPressed)
        {
            if (isPressed) Interact();
        }

        public void Interact()
        {
            currentState.Interact(this);
        }

        public void Carry(CarriableObject carriableObject)
        {
            currentState.Carry(this, carriableObject);
        }

        public void Throw()
        {
            currentState.Throw(this);
        }

        public HandsState GetState()
        {
            return currentState;
        }

        public void SetState(HandsState newState)
        {
            currentState.Exit(this);
            currentState = newState;
            currentState.Enter(this);
        }
    }
}
