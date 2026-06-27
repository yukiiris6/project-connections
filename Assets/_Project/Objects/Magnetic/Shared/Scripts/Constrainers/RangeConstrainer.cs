using System;
using ProjectConnections.Magnetic;
using ProjectConnections.ObjectShared;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectConnections.Magnetic
{
    public class RangeConstrainer : MonoBehaviour, Constrainer
    {
        [SerializeField, Required] float maxDistance = 1f;

        Vector2 originalPosition;

        void Awake()
        {
            originalPosition = transform.position;
        }

        public Vector2 GetConstrained(Vector2 targetPosition)
        {
            Vector2 direction = (targetPosition - originalPosition).normalized;

            float distanceToTravel = Vector2.Distance(originalPosition, targetPosition);
            if (distanceToTravel >= maxDistance)
            {
                targetPosition = originalPosition + (direction * maxDistance);
            }

            return targetPosition;
        }

        public float GetMaxDistance()
        {
            return maxDistance;
        }
    }
}
