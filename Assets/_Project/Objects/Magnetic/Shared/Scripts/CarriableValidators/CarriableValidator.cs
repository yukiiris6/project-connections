using ProjectConnections.ObjectShared;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectConnections.Magnetic
{
    public class CarriableValidator : MonoBehaviour
    {
        [SerializeField, Required] CarriableObject carriableObject;
        [SerializeField, Required] Mover mover;

        void OnEnable()
        {
            carriableObject.OnCarryChanged += HandleCarry;
        }

        void Update()
        {
            ValidateCarryOnTrigger();
        }

        void ValidateCarryOnTrigger()
        {
            carriableObject.ToggleTrigger(mover.IsMoving());
        }

        void HandleCarry(bool isCarrying)
        {
            if (isCarrying) mover.Stop();
        }
    }
}
