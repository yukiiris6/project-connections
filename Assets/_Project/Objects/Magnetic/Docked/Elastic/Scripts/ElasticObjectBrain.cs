using ProjectConnections.Magnetic.Elastic.States;
using ProjectConnections.Magnetic.States;
using ProjectConnections.Magnetic.Modules;
using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.Magnetic.Anchored;

namespace ProjectConnections.Magnetic.Docked
{
    public class ElasticObjectBrain : MonoBehaviour, IContext, MagnetismModule, AnchorModule
    {
        [field: SerializeField] public Mover Mover { get; private set; }
        [field: SerializeField] public Rotator Rotator { get; private set; }
        [field: SerializeField, Required] public Constrainer Constrainer { get; private set; }
        [field: SerializeField] public Rigidbody2D Rigidbody { get; private set; }
        [field: SerializeField] public Presenter Presenter { get; private set; }
        [field: SerializeField] public AnchorRange AnchorRange { get; private set; }

        IState currentState = new ElasticPulled();

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
            if (currentState is StateAnchorModule anchorStateModule)
            {
                anchorStateModule.MagnetizeAnchor(this);
            }
        }

        public void DemagnetizeAnchor()
        {
            if (currentState is StateAnchorModule anchorStateModule)
            {
                anchorStateModule.DemagnetizeAnchor(this);
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
