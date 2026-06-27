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
        [ShowInInspector, ReadOnly] SocketConnector anchoredSocketConnector;

        public void Energize()
        {
            anchoredSocketConnector.ElectricityReceiver.SetProvider(electricityProvider);
        }

        public void Deenergize()
        {
            anchoredSocketConnector.ElectricityReceiver.SetProvider(null);
            anchoredSocketConnector = null;
        }

        public Vector2? GetConnectionPosition()
        {
            if (anchoredSocketConnector != null) return anchoredSocketConnector.ConnectionAnchor.position;
            return null;
        }

        public Quaternion? GetConnectionRotation()
        {
            if (anchoredSocketConnector != null) return anchoredSocketConnector.ConnectionRotation;
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
            if (!socketConnector.IsConnected())
            {
                anchoredSocketConnector = socketConnector;
            }
        }
    }
}
