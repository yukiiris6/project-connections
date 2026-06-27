using ProjectConnections.Magnetic.Anchored.States;
using ProjectConnections.Magnetic.Modules;
using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.Anchored
{
    public class AnchoredObjectBrain : MonoBehaviour, IContext, AnchorModule
    {
        [field: SerializeField, Required] public Mover Mover { get; private set; }
        [field: SerializeField, Required] public Rotator Rotator { get; private set; }
        [field: SerializeField, Required] public Constrainer Constrainer { get; private set; }
        [field: SerializeField, Required] public Rigidbody2D Rigidbody { get; private set; }
        [field: SerializeField, Required] public Presenter Presenter { get; private set; }
        [field: SerializeField, Required] public AnchorRange AnchorRange { get; private set; }

        IState currentState = new AnchoredResting();

        public void Magnetize(Vector2 destination)
        {
            currentState.Magnetize(this, destination);
        }

        public void Demagnetize()
        {
            currentState.Demagnetize(this);
        }

        public void MagnetizeAnchor()
        {
            if (currentState is StateAnchorModule stateDockedModule)
            {
                stateDockedModule.MagnetizeAnchor(this);
            }
        }

        public void DemagnetizeAnchor()
        {
            if (currentState is StateAnchorModule stateDockedModule)
            {
                stateDockedModule.DemagnetizeAnchor(this);
            }
        }

        void IContext.SetState(IState newState)
        {
            currentState.Exit(this);
            currentState = newState;
            currentState.Enter(this);
        }
    }
}
