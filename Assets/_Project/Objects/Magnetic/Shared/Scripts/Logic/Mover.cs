using System;
using ProjectConnections.Magnetic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic
{
    public class Mover : MonoBehaviour
    {
        [SerializeField, Required] CollisionHandler collisionHandler;
        [SerializeField, Required] Rigidbody2D myRigidbody;
        [SerializeField] float pullMultiplier = 1.1f;
        [SerializeField] float nextPositionThreshold = .25f;

        public event Action<float> OnDestinationReached;

        Vector2 lastPosition;
        Vector2 targetPosition;
        Vector2 lastVelocity;
        bool shouldMove;

        #region Unity Lifecycle
        void Awake()
        {
            lastPosition = transform.position;
        }

        void FixedUpdate()
        {
            ExecuteMovement();
        }
        #endregion

        #region Handlers
        public void MoveTo(Vector2 destination)
        {
            targetPosition = destination;
            shouldMove = true;
            lastPosition = transform.position;
        }

        public void SnapTo(Vector2 snapPosition)
        {
            transform.position = snapPosition;
        }

        public void Stop()
        {
            shouldMove = false;
            lastVelocity = Vector2.zero;
            myRigidbody.linearVelocity = Vector2.zero;
            float distance = Vector2.Distance(lastPosition, targetPosition);
            OnDestinationReached?.Invoke(distance);
        }

        public bool IsMoving()
        {
            return shouldMove;
        }

        public bool IsSameAsCurrentPosition(Vector2 destination)
        {
            return Vector2.Distance(destination, transform.position) <= nextPositionThreshold;
        }
        #endregion

        #region Logic
        void ExecuteMovement()
        {
            if (!shouldMove) return;

            Vector2 currentPosition = transform.position;
            Vector2 direction = targetPosition - currentPosition;
            Vector2 newVelocity = GetNewVelocity(currentPosition, direction);

            bool hasReachedDestination = ValidateReachedDestination();
            if (hasReachedDestination)
            {
                Stop();
                return;
            }

            myRigidbody.linearVelocity = newVelocity;
            lastVelocity = newVelocity;
        }

        Vector2 GetNewVelocity(Vector2 currentPosition, Vector2 direction)
        {
            Vector2 normalizedDirection = direction.normalized;
            Vector2 newVelocity = lastVelocity + (normalizedDirection * pullMultiplier);

            float distance = Vector2.Distance(targetPosition, currentPosition);
            float maxSafeSpeed = distance / Time.fixedDeltaTime;

            if (newVelocity.magnitude > maxSafeSpeed) return (normalizedDirection * maxSafeSpeed);
            return newVelocity;
        }

        bool ValidateReachedDestination()
        {
            Vector2 currentPosition = transform.position;
            float distance = Vector2.Distance(currentPosition, targetPosition);
            return distance < 0.15f;
        }
        #endregion
    }
}
