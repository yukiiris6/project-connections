using UnityEngine;
using Sirenix.OdinInspector;
using System;

namespace ProjectConnections.Electric
{
    public class SocketConnector : MonoBehaviour
    {
        [SerializeField, Required] Transform connectionAnchor;
        [SerializeField, Required] ElectricityReceiver electricityReceiver;
        [SerializeField] Transform connectedPlug;

        public Transform ConnectionAnchor { get; private set; }
        public Quaternion ConnectionRotation { get; private set; }
        public event Action OnDisconnect;

        void Awake()
        {
            ConnectionAnchor = connectionAnchor;
            ConnectionRotation = connectionAnchor.rotation;
            ValidateConnectedPlug();
        }

        void Update()
        {
            ValidatePlugConnection();
        }

        void ValidateConnectedPlug()
        {
            if (connectedPlug == null) return;
            var provider = connectedPlug.GetComponent<ElectricityProvider>();
            if (provider == null) return;
            connectedPlug.rotation = ConnectionRotation;
            connectedPlug.position = ConnectionAnchor.position;
            ConnectPlug(provider);
        }

        public void ConnectPlug(ElectricityProvider electricityProvider)
        {
            electricityReceiver.SetProvider(electricityProvider);
            connectedPlug = electricityProvider.transform;
        }

        public bool IsConnected()
        {
            return electricityReceiver.Provider != null;
        }

        void ValidatePlugConnection()
        {
            if (connectedPlug == null) return;
            if (connectedPlug.position == connectionAnchor.position) return;
            electricityReceiver.SetProvider(null);
            connectedPlug = null;
            OnDisconnect?.Invoke();
        }
    }
}
