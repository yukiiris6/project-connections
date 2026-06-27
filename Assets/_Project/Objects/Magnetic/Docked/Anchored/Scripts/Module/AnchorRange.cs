using System;
using ProjectConnections.Magnetic.Anchored;
using ProjectConnections.ObjectShared;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectConnections.Magnetic
{
    public class AnchorRange : MonoBehaviour
    {
        [SerializeField, Required] float maxDistance = 1f;

        Vector2 originalPosition;

        void Awake()
        {
            originalPosition = transform.position;
        }

        public Vector2 ConstrainMaxDistance(Vector2 targetPosition)
        {
            Vector2 direction = (targetPosition - originalPosition).normalized;

            float distanceToTravel = Vector2.Distance(originalPosition, targetPosition);
            if (distanceToTravel >= maxDistance)
            {
                targetPosition = originalPosition + (direction * maxDistance);
            }

            return targetPosition;
        }

        public Vector2 GetOriginalPosition()
        {
            return originalPosition;
        }

        public float GetMaxDistance()
        {
            return maxDistance;
        }
    }
}
