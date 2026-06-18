using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.Electric;

namespace ProjectConnections.Magnetism
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
                SocketConnector socketConnector = hit.collider.gameObject.GetComponent<SocketConnector>();
                if (socketConnector != null)
                {
                    if (!socketConnector.IsConnected)
                    {
                        AnchoredSocketConnector = socketConnector;
                        return socketConnector;
                    }
                }
            }
            return null;
        }
    }
}
