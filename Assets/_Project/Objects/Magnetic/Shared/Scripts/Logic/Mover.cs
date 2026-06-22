using System;
using ProjectConnections.Magnetic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic
{
    public class Mover : MonoBehaviour
    {
        [SerializeField, Required] CollisionHandler collisionHandler;
        [SerializeField, Required] float pullMultiplier = 1.1f;
        [SerializeField, Required] float defaultStopDistance = 1f;
        [SerializeField, Required] float preciseStopDistance = .25f;

        public event Action OnDestinationReached;

        Vector2 targetPosition;
        Vector2 lastVelocity;
        Quaternion originalRotation;
        float appliedStopDistance;
        bool shouldUseCollision;
        bool shouldMove;

        #region Unity Lifecycle
        void Awake()
        {
            appliedStopDistance = defaultStopDistance;
            originalRotation = transform.rotation;
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
        }

        public void SnapTo(Vector2 snapPosition)
        {
            transform.position = snapPosition;
        }

        public void Stop()
        {
            shouldMove = false;
            lastVelocity = Vector2.zero;
        }

        public bool IsSameAsCurrentPosition(Vector2 destination)
        {
            return Vector2.Distance(destination, transform.position) <= (defaultStopDistance + .25f);
        }

        public void UsePreciseArrival(bool shouldBePrecise)
        {
            appliedStopDistance = shouldBePrecise ? preciseStopDistance : defaultStopDistance;
        }

        public void UseCollision(bool shouldUse)
        {
            shouldUseCollision = shouldUse;
        }

        public void RotateTowardsTarget()
        {
            Vector2 currentPosition = transform.position;
            Vector2 direction = targetPosition - currentPosition;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float snappedAngle = Mathf.Round(angle / 90f) * 90f;
            SetRotation(Quaternion.Euler(0, 0, snappedAngle));
        }

        public void ResetRotation()
        {
            SetRotation(originalRotation);
        }

        public void SetRotation(Quaternion newRotation)
        {
            ValidateChildrenRotation();
            transform.localRotation = Quaternion.Euler(newRotation.eulerAngles - transform.parent.rotation.eulerAngles);
        }
        #endregion

        #region Logic
        void ExecuteMovement()
        {
            if (!shouldMove) return;

            Vector2 currentPosition = transform.position;
            Vector2 direction = targetPosition - currentPosition;
            Vector2 newVelocity = GetNewVelocity(currentPosition, direction);
            Vector2 frameVelocity = newVelocity * Time.fixedDeltaTime;

            if (shouldUseCollision)
            {
                RaycastHit2D hit = collisionHandler.GetBoxCastInDirection(direction, frameVelocity.magnitude);

                bool hasColliderInDirection = hit.collider != null;
                if (hasColliderInDirection)
                {
                    float safeDistance = Mathf.Max(0f, hit.distance - 0.015f);
                    transform.position = currentPosition + (direction.normalized * safeDistance);
                    Stop();
                    return;
                }
            }

            bool hasReachedDestination = ValidateReachedDestination();
            if (hasReachedDestination)
            {
                Stop();
                OnDestinationReached?.Invoke();
            }

            transform.Translate(frameVelocity, Space.World);
            lastVelocity = newVelocity;
        }

        Vector2 GetNewVelocity(Vector2 currentPosition, Vector2 direction)
        {
            Vector2 normalizedDirection = direction.normalized;
            Vector2 newVelocity = lastVelocity + (normalizedDirection * pullMultiplier);

            float distance = Vector2.Distance(targetPosition, currentPosition);
            float maxSafeSpeed = (distance - appliedStopDistance) / Time.fixedDeltaTime;

            if (newVelocity.magnitude > maxSafeSpeed) return (normalizedDirection * maxSafeSpeed);
            return newVelocity;
        }

        bool ValidateReachedDestination()
        {
            Vector2 currentPosition = transform.position;
            float distance = Vector2.Distance(currentPosition, targetPosition);
            return distance < appliedStopDistance + 0.001f;
        }

        void ValidateChildrenRotation()
        {
            if (transform.childCount > 0)
            {
                foreach (Transform child in transform)
                {
                    if (child.CompareTag("Player"))
                    {
                        child.SetParent(null);
                        child.rotation = Quaternion.identity;
                    }
                }
            }
        }
        #endregion
    }
}
