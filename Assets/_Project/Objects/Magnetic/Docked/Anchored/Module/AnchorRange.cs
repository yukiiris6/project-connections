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
        [SerializeField, Required] float stopDistance = 1f;

        Vector2 originalPosition;

        void Awake()
        {
            originalPosition = transform.position;
        }

        public Vector2 ConstrainTargetPosition(Vector2 targetPosition)
        {
            Vector2 direction = (targetPosition - originalPosition).normalized;
            Vector2 constrainedTargetPosition = targetPosition - (direction * stopDistance);

            float distanceToTravel = Vector2.Distance(originalPosition, constrainedTargetPosition);
            if (distanceToTravel >= maxDistance)
            {
                constrainedTargetPosition = originalPosition + (direction * maxDistance);
            }

            return constrainedTargetPosition;
        }

        public Vector2 GetOriginalPosition()
        {
            return originalPosition;
        }
    }
}
