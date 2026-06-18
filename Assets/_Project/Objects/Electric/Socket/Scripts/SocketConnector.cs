using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Electric
{
    public class SocketConnector : MonoBehaviour
    {
        [field: SerializeField] public Transform ConnectionAnchor { get; private set; }

        public Vector3 ConnectionRotation { get; private set; }
        public bool IsConnected { get; private set; }

        void Awake()
        {
            ConnectionRotation = ConnectionAnchor.rotation.eulerAngles;
        }

        public void ToggleConnected(bool value)
        {
            IsConnected = value;
        }
    }
}