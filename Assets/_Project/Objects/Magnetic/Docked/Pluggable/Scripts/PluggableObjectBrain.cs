using ProjectConnections.Magnetic.Anchored.States;
using ProjectConnections.Magnetic.Modules;
using ProjectConnections.Magnetic.Pluggable.States;
using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.Electric;

namespace ProjectConnections.Magnetic
{
    public class PluggableObjectBrain : MonoBehaviour, IContext, MagnetismModule, DockedModule, EnergizerModule, CarriableModule
    {
        [field: SerializeField] public Mover Mover { get; private set; }
        [field: SerializeField] public SoundPlayer SoundPlayer { get; private set; }
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
        [field: SerializeField] public Presenter Presenter { get; private set; }
        [field: SerializeField] public Energizer Energizer { get; private set; }
        [field: SerializeField] public CarriableObject CarriableObject { get; private set; }
        [SerializeField, Optional] SocketConnector defaultSocket;

        public Vector2 OriginalPosition { get; private set; }

        IState currentState;

        void Start()
        {
            OriginalPosition = transform.position;
            if (defaultSocket != null)
            {
                Energizer.SetSocketConnector(defaultSocket);
                currentState = new PluggablePlugged();
                currentState.Enter(this);
            }
            else
            {
                currentState = new PluggableResting();
            }
        }

        public void Magnetize(Vector2 destination)
        {
            currentState.Magnetize(this, destination);
        }

        public void Demagnetize()
        {
            currentState.Demagnetize(this);
        }

        public void MagnetizeDock()
        {
            if (currentState is StateDockedModule stateDockedModule)
            {
                stateDockedModule.MagnetizeDock(this);
            }
        }

        public void DemagnetizeDock()
        {
            if (currentState is StateDockedModule stateDockedModule)
            {
                stateDockedModule.DemagnetizeDock(this);
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
