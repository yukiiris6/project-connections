using System;
using ProjectConnections.Magnetic.Anchored;
using ProjectConnections.ObjectShared;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectConnections.Magnetic
{
    public class PlugCarryRange : MonoBehaviour
    {
        [field: SerializeField, Required] public CarriableObject CarriableObject { get; private set; }
        [SerializeField, Required] Mover mover;
        [SerializeField] float grabMaxDistance = .75f;
        [SerializeField] float carryMaxDistance = .5f;

        Vector2 originalPosition;
        bool isReturning;

        void Awake()
        {
            originalPosition = transform.position;
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
