using ProjectConnections.Magnetic.Modules;
using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.ObjectShared;

namespace ProjectConnections.Magnetic
{
    public class BasicObjectBrain : MonoBehaviour, IContext, MagnetismModule, CarriableModule
    {
        [field: SerializeField, Required] public Mover Mover { get; private set; }
        [field: SerializeField, Required] public Rotator Rotator { get; private set; }
        [field: SerializeField, Required] public Constrainer Constrainer { get; private set; }
        [field: SerializeField, Required] public Rigidbody2D Rigidbody { get; private set; }
        [field: SerializeField, Required] public Presenter Presenter { get; private set; }
        [field: SerializeField, Required] public CarriableObject CarriableObject { get; private set; }

        IState currentState = new Resting();

        public void Magnetize(Vector2 destination)
        {
            currentState.Magnetize(this, destination);
        }

        public void Demagnetize()
        {
            currentState.Demagnetize(this);
        }

        void IContext.SetState(IState newState)
        {
            currentState.Exit(this);
            currentState = newState;
            currentState.Enter(this);
        }
    }
}
