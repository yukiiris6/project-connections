using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.Electric;

namespace ProjectConnections.Magnetic
{
    public class Energizer : MonoBehaviour
    {
        [SerializeField, Required] ElectricityProvider electricityProvider;
        [SerializeField, Required] CircleCollider2D connectionTriggerCollider;
        [SerializeField, Required] LayerMask socketLayer;

        public ElectricityProvider ElectricityProvider { get; private set; }
        public SocketConnector AnchoredSocketConnector { get; private set; }

        public void EnergizeProvider()
        {
            ElectricityGenerator newGenerator = AnchoredSocketConnector.GetComponent<ElectricityGenerator>();
            electricityProvider.ConnectToGenerator(newGenerator);
            AnchoredSocketConnector.ToggleConnected(true);
        }

        public void DeenergizeProvider()
        {
            electricityProvider.DisconnectFromGenerator();
            AnchoredSocketConnector.ToggleConnected(false);
            AnchoredSocketConnector = null;
        }

        public Vector2? GetConnectionPosition()
        {
            if (AnchoredSocketConnector != null) return AnchoredSocketConnector.ConnectionAnchor.position;
            return null;
        }

        public SocketConnector SearchFreeSocket()
        {
            RaycastHit2D hit = Physics2D.CircleCast(
                connectionTriggerCollider.bounds.center,
                connectionTriggerCollider.radius,
                Vector2.zero,
                0f,
                socketLayer
            );

            if (hit.collider)
            {
                SocketConnector socketConnector = hit.collider.GetComponent<SocketConnector>();
                if (socketConnector != null)
                {
                    SetSocketConnector(socketConnector);
                    return socketConnector;
                }
            }

            return null;
        }

        public void SetSocketConnector(SocketConnector socketConnector)
        {
            if (!socketConnector.IsConnected)
            {
                AnchoredSocketConnector = socketConnector;
            }
        }
    }
}
