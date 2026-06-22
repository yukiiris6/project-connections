using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Electric
{
    public class SocketConnector : MonoBehaviour
    {
        [field: SerializeField, Required] public Transform ConnectionAnchor { get; private set; }
        [field: SerializeField, Required] public ElectricityReceiver ElectricityReceiver { get; private set; }

        public Quaternion ConnectionRotation { get; private set; }

        void Awake()
        {
            ConnectionRotation = ConnectionAnchor.rotation;
        }

        public bool IsConnected()
        {
            return ElectricityReceiver.Provider != null;
        }
    }
}
