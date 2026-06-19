using ProjectConnections.Magnetic.Anchored.States;
using ProjectConnections.Magnetic.Modules;
using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.Anchored
{
    public class AnchoredObjectBrain : MonoBehaviour, IContext, MagnetismModule, DockedModule
    {
        [field: SerializeField] public Mover Mover { get; private set; }
        [field: SerializeField] public SoundPlayer SoundPlayer { get; private set; }
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
        [field: SerializeField] public Presenter Presenter { get; private set; }

        public Vector2 OriginalPosition { get; private set; }
        IState currentState = new AnchoredResting();

        void Awake()
        {
            OriginalPosition = transform.position;
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