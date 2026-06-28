using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectConnections.ObjectShared
{
    public class CarriableTrigger : MonoBehaviour
    {
        [SerializeField, Required] CarriableObject carriableObject;

        public CarriableObject GetCarriableObject()
        {
            return carriableObject;
        }
    }
}
