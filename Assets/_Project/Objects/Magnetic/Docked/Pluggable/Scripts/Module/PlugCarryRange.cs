using System;
using ProjectConnections.Magnetic.Anchored;
using ProjectConnections.ObjectShared;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectConnections.Magnetic
{
    public class PlugCarryRange : MonoBehaviour
    {
        [SerializeField, Required] CarriableObject CarriableObject;
        [SerializeField, Required] AnchorRange anchorRange;
        [SerializeField, Required] Mover mover;
        [SerializeField] float grabThreshold = 2f;
        [SerializeField] float carryThreshold = 1f;
        [SerializeField] float minDistance;

        Vector2 originalPosition;
        bool isReturning;
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

        public void SetReturning(bool value)
        {
            isReturning = value;
        }

        void ValidateCarryOnTrigger()
        {
            if (!mover.IsMoving() || isReturning)
            {
                if (CarriableObject.ShouldCarryOnTrigger) CarriableObject.SetCarryOnTrigger(false);
                return;
            }

            Vector2 currentPosition = transform.position;
            float distanceTravelled = Vector2.Distance(currentPosition, originalPosition);

            bool shouldCarryOnTrigger = false;
            if (distanceTravelled < grabMaxDistance) shouldCarryOnTrigger = true;
            CarriableObject.SetCarryOnTrigger(shouldCarryOnTrigger);
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
