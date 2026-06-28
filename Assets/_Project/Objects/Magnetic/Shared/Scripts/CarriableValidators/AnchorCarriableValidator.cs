using System;
using ProjectConnections.Magnetic;
using ProjectConnections.ObjectShared;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectConnections.Magnetic
{
    public class AnchorCarriableValidator : MonoBehaviour
    {
        [SerializeField, Required] CarriableObject CarriableObject;
        [SerializeField, Required] RangeConstrainer anchorRange;
        [SerializeField, Required] Mover mover;
        [SerializeField] float grabThreshold = 2f;
        [SerializeField] float carryThreshold = 1f;
        [SerializeField] float minDistance = 1f;

        Vector2 originalPosition;
        float grabMaxDistance;
        float carryMaxDistance;

        void Awake()
        {
            originalPosition = transform.position;
            float maxDistance = anchorRange.GetMaxDistance();
            grabMaxDistance = Mathf.Max(maxDistance - grabThreshold, minDistance);
            carryMaxDistance = Mathf.Max(maxDistance - carryThreshold, minDistance);
        }

        void Update()
        {
            ValidateCarryOnTrigger();
            ValidateCarriableDistance();
        }

        void ValidateCarryOnTrigger()
        {
            bool isReturning = mover.GetTargetPosition() == originalPosition;

            if (!mover.IsMoving() || isReturning)
            {
                if (CarriableObject.TriggerIsActive) CarriableObject.ToggleTrigger(false);
                return;
            }

            Vector2 currentPosition = transform.position;
            float distanceTravelled = Vector2.Distance(currentPosition, originalPosition);

            bool shouldEnableTrigger = false;
            if (distanceTravelled < grabMaxDistance) shouldEnableTrigger = true;
            CarriableObject.ToggleTrigger(shouldEnableTrigger);
        }

        void ValidateCarriableDistance()
        {
            Vector2 currentPosition = transform.position;
            float distanceFromOriginal = Vector2.Distance(currentPosition, originalPosition);
            if (CarriableObject.IsBeingCarried && distanceFromOriginal >= carryMaxDistance)
            {
                CarriableObject.LetGo();
            }
        }
    }
}
