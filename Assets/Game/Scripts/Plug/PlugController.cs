using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PlugVisuals))]
public class PlugController : MonoBehaviour
{
    [Header("Magnetism Settings")]
    [SerializeField] float pullMultiplier = 1.1f;
    [SerializeField] float maxDistance = 1f;
    [SerializeField] float minimumPlayerDistance = 1f;
    [SerializeField] float mouseStopDistance = 1f;
    [SerializeField] float returnStopDistance = 0.5f;

    [Header("Collision")]
    [SerializeField] LayerMask wallLayer;

    PlugVisuals plugVisuals;
    Vector2 originalPosition;
    Vector2 targetPosition;
    Vector2 currentVelocity;
    BoxCollider2D myCollider;

    public bool IsMoving { get; private set; } = false;
    bool isReturning = false;
    bool shouldApplyStopDistance = false;

    #region Unity Lifecycle
    void Awake()
    {
        plugVisuals = GetComponent<PlugVisuals>();
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
    public void Magnetize(Vector2 mousePosition, Vector2 playerPosition)
    {
        if (IsMoving) return;

        float distanceToMouse = Vector2.Distance(transform.position, mousePosition);
        float distanceToPlayer = Vector2.Distance(transform.position, playerPosition);

        Vector2 playerToPlugDirection = (playerPosition - (Vector2)transform.position).normalized;
        Vector2 mouseToPlugDirection = (mousePosition - (Vector2)transform.position).normalized;
        float aligment = Vector2.Dot(playerToPlugDirection, mouseToPlugDirection);

        if (distanceToMouse < mouseStopDistance) return;
        if (distanceToPlayer < minimumPlayerDistance) return;
        if (aligment < 0.95f) return;

        targetPosition = mousePosition;
        isReturning = false;
        IsMoving = true;
        shouldApplyStopDistance = true;
    }

    public void ConnectToSocket(Vector2 newTargetPosition)
    {
        targetPosition = newTargetPosition;
        isReturning = false;
        IsMoving = true;
        shouldApplyStopDistance = false;
    }

    public void CancelMagnetism()
    {
        if (IsMoving) return;
        if (targetPosition == originalPosition) return;

        targetPosition = originalPosition;
        isReturning = true;
        IsMoving = true;
        shouldApplyStopDistance = false;
    }
    #endregion

    #region Logic
    void Move()
    {
        if (!IsMoving) return;

        float appliedStopDistance = shouldApplyStopDistance ? mouseStopDistance : 0;
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

        float appliedDistanceToSnap = targetPosition == originalPosition ? returnStopDistance : mouseStopDistance;
        if (nextDistance <= appliedDistanceToSnap + 0.001f || currentDistance <= appliedDistanceToSnap + 0.001f)
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

        if (hit.collider == null || isReturning)
        {
            transform.Translate(translation);
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
        plugVisuals.PlayScreenShake();
    }
    #endregion
}