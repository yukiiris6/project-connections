using System;
using ProjectConnections.Magnetic;
using UnityEngine;
using Sirenix.OdinInspector;

public class Mover : MonoBehaviour
{
    [SerializeField, Required] CollisionHandler collisionHandler;
    [SerializeField, Required] float pullMultiplier = 1.1f;
    [SerializeField, Required] float defaultStopDistance = 1f;
    [SerializeField, Required] float preciseStopDistance = .25f;

    public event Action OnDestinationReached;

    Vector2 targetPosition;
    Vector2 lastVelocity;
    bool shouldMove = false;
    float appliedStopDistance;
    bool shouldUseCollision;

    #region Unity Lifecycle
    void Awake()
    {
        appliedStopDistance = defaultStopDistance;
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
    #endregion
}