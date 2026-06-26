using ProjectConnections.Magnetic.Anchored.States;
using ProjectConnections.Magnetic.Modules;
using ProjectConnections.Magnetic.Pluggable.States;
using ProjectConnections.Magnetic.States;
using ProjectConnections.ObjectShared;
using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.Electric;
using ProjectConnections.Magnetic.Anchored;

namespace ProjectConnections.Magnetic
{
    public class PluggableObjectBrain : MonoBehaviour, IContext, MagnetismModule, AnchorModule, PlugModule
    {
        [Header("Magnetism")]
        [field: SerializeField] public Mover Mover { get; private set; }
        [field: SerializeField] public Rotator Rotator { get; private set; }
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
        [field: SerializeField] public Presenter Presenter { get; private set; }

        [Header("Anchor")]
        [field: SerializeField] public AnchorRange AnchorRange { get; private set; }

        [Header("Plug")]
        [field: SerializeField] public Energizer Energizer { get; private set; }
        [field: SerializeField] public PlugCarryRange PlugCarryRange { get; private set; }

        [Header("Optional")]
        [SerializeField, Optional] SocketConnector defaultSocket;

        IState currentState;

        void Start()
        {
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

        public void MagnetizeAnchor()
        {
            if (currentState is not StateAnchorModule stateDockedModule) return;
            stateDockedModule.MagnetizeAnchor(this);
        }

        public void DemagnetizeAnchor()
        {
            if (currentState is not StateAnchorModule stateDockedModule) return;
            stateDockedModule.DemagnetizeAnchor(this);
        }

        void IContext.SetState(IState newState)
        {
            currentState.Exit(this);
            currentState = newState;
            currentState.Enter(this);
        }
    }
}
