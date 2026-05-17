using UnityEngine;

public class PlugController : MonoBehaviour
{
    [Header("Magnetism Settings")]
    [SerializeField] float pullMultiplier = 1.1f;
    [SerializeField] float stopDistance = 1f;
    [SerializeField] float maxDistance = 1f;

    [Header("Collision")]
    [SerializeField] LayerMask wallLayer;

    Vector2 originalPosition;
    Vector2 targetPosition;
    Vector2 currentVelocity;
    BoxCollider2D myCollider;
    public bool IsMoving { get; private set; } = false;
    bool shouldApplyStopDistance = false;

    #region Unity Lifecycle
    void Awake()
    {
        myCollider = GetComponent<BoxCollider2D>();
        originalPosition = transform.position;
        targetPosition = originalPosition;
    }

    void FixedUpdate()
    {
        Move();
    }
    #endregion

    #region Magnetization
    public void Magnetize(Vector2 newTargetPosition, bool isSocket)
    {
        if (IsMoving && !isSocket) return;
        targetPosition = newTargetPosition;
        IsMoving = true;
        shouldApplyStopDistance = !isSocket;
    }

    public void CancelMagnetism()
    {
        if (IsMoving) return;
        if (targetPosition == originalPosition) return;
        targetPosition = originalPosition;
        IsMoving = true;
        shouldApplyStopDistance = false;
    }
    #endregion

    #region Logic
    void Move()
    {
        if (!IsMoving) return;

        float appliedStopDistance = shouldApplyStopDistance ? stopDistance : 0;
        float currentDistance = Vector2.Distance(transform.position, targetPosition);

        currentVelocity = GetVelocity(appliedStopDistance, currentDistance);
        Vector2 translation = currentVelocity * Time.fixedDeltaTime;

        Vector2 nextPosition = (Vector2)transform.position + translation;
        float nextDistance = Vector2.Distance(nextPosition, targetPosition);
        float distanceTravelled = Vector2.Distance(originalPosition, nextPosition);

        if (distanceTravelled > maxDistance)
        {
            SnapToMaxDistance();
            return;
        }

        if (nextDistance <= appliedStopDistance || currentDistance <= appliedStopDistance)
        {
            SnapToTarget(appliedStopDistance);
            return;
        }

        Vector2 direction = translation.normalized;
        float moveDistance = translation.magnitude;

        RaycastHit2D hit = Physics2D.BoxCast(
            myCollider.bounds.center,
            myCollider.bounds.size,
            0f,
            direction,
            moveDistance,
            wallLayer
        );

        if (hit.collider == null)
        {
            transform.Translate(translation, Space.World);
        }
        else
        {
            transform.position = (Vector2)transform.position + (direction * hit.distance);
            StopMovement();
        }
    }

    Vector2 GetVelocity(float appliedStopDistance, float currentDistance)
    {
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        Vector2 newVelocity = currentVelocity + (direction * pullMultiplier);

        float maxSafeSpeed = (currentDistance - appliedStopDistance) / Time.fixedDeltaTime;
        if (newVelocity.magnitude > maxSafeSpeed) newVelocity = direction * maxSafeSpeed;

        return newVelocity;
    }

    void SnapToTarget(float appliedStopDistance)
    {
        Vector2 directionFromPlayerToPlug = ((Vector2)transform.position - targetPosition).normalized;
        transform.position = targetPosition + (directionFromPlayerToPlug * appliedStopDistance);
        StopMovement();
    }

    void SnapToMaxDistance()
    {
        Vector2 directionToTarget = (targetPosition - originalPosition).normalized;
        transform.position = originalPosition + (directionToTarget * maxDistance);
        StopMovement();
    }

    void StopMovement()
    {
        currentVelocity = Vector2.zero;
        IsMoving = false;
    }
    #endregion
}