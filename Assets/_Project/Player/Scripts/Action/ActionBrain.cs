using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.ObjectShared;

namespace ProjectConnections.Player
{
    public class ActionBrain : MonoBehaviour, ActionContext
    {
        [SerializeField] PlayerController playerController;

        [field: SerializeField, Required] public Carrier Carrier { get; private set; }
        [field: SerializeField, Required] public CarriableFinder CarriableFinder { get; private set; }
        [field: SerializeField, Required] public InteractableFinder InteractableFinder { get; private set; }
        [field: SerializeField, Required] public MagnetAiming MagnetAiming { get; private set; }
        [field: SerializeField, Required] public ActionAnimation ActionAnimation { get; private set; }
        [field: SerializeField, Required] public InteractableController InteractableController { get; private set; }

        ActionState currentState = new Resting();

        void Awake()
        {
            currentState.Enter(this);
        }

        public void Use()
        {
            currentState.Use(this);
        }

        public void Interact()
        {
            currentState.Interact(this);
        }

        public void Magnetize(bool isPressed)
        {
            currentState.Magnetize(this, isPressed);
        }

        public void SetState(ActionState newState)
        {
            currentState.Exit(this);
            currentState = newState;
            currentState.Enter(this);
        }
    }
}
