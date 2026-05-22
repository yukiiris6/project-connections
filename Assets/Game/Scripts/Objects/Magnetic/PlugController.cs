using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(PlugVisuals))]
[RequireComponent(typeof(PlugSoundPlayer))]
public class PlugController : MonoBehaviour
{
    [Header("Magnetism Settings")]
    [SerializeField] float pullMultiplier = 1.1f;
    [SerializeField] float maxDistance = 1f;
    [SerializeField] float minimumPlayerDistance = 1f;
    [SerializeField] float playerStopDistance = 1f;
    [SerializeField] float snapDistance = 1f;
    [SerializeField] float returnSnapDistance = 0.5f;

    [Header("Collision")]
    [SerializeField] LayerMask wallLayer;
    [SerializeField] BoxCollider2D mainCollider;
    [SerializeField] LayerMask ignoreWhileMagnetizing;
    [SerializeField] Transform spriteTransform;

    PlugVisuals plugVisuals;
    PlugSoundPlayer plugSoundPlayer;
    Vector2 originalPosition;
    Vector2 targetPosition;
    Vector2 currentVelocity;
    BoxCollider2D[] allColliders;
    SocketController targetSocket;

    public bool IsMoving { get; private set; } = false;
    bool isReturning = false;
    bool shouldApplyStopDistance = false;
    bool isInContainer = true;

    #region Unity Lifecycle
    void Awake()
    {
        plugVisuals = GetComponent<PlugVisuals>();
        plugSoundPlayer = GetComponent<PlugSoundPlayer>();
        allColliders = GetComponents<BoxCollider2D>();
        originalPosition = transform.position;
        targetPosition = originalPosition;
    }

    void Start()
    {
        ValidatePosition();
    }

    void FixedUpdate()
    {
        Move();
    }
    #endregion

    #region Magnetization
    public void Magnetize(Vector2 playerPosition)
    {
        if (IsMoving) return;
        if (targetSocket || !isInContainer)
        {
            CancelMagnetism();
            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, playerPosition);
        if (distanceToPlayer < minimumPlayerDistance) return;

        targetPosition = playerPosition;
        isReturning = false;
        IsMoving = true;
        shouldApplyStopDistance = true;
        targetSocket = null;
        foreach (var collider in allColliders)
        {
            collider.excludeLayers = ignoreWhileMagnetizing;
        }
    }

    public void ConnectToSocket(Vector2 newTargetPosition, SocketController socket)
    {
        targetPosition = newTargetPosition;
        isReturning = false;
        IsMoving = true;
        shouldApplyStopDistance = false;
        targetSocket = socket;
        foreach (var collider in allColliders)
        {
            collider.excludeLayers = ignoreWhileMagnetizing;
        }
    }

    public void CancelMagnetism()
    {
        if (IsMoving) return;
        if (targetPosition == originalPosition) return;

        targetPosition = originalPosition;
        isReturning = true;
        IsMoving = true;
        shouldApplyStopDistance = false;

        if (targetSocket)
        {
            targetSocket.ChangeActivationState(false);
            targetSocket = null;
        }
    }

    public void SetStartState(SocketController socket, Vector2 newTargetPosition)
    {
        targetPosition = newTargetPosition;
        transform.position = newTargetPosition;
        targetSocket = socket;
    }
    #endregion

    #region Logic
    void Move()
    {
        if (!IsMoving) return;

        float appliedStopDistance = shouldApplyStopDistance ? playerStopDistance : 0;
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

        bool isSocketAndShouldSnap = targetSocket != null && (currentDistance <= snapDistance || nextDistance <= snapDistance);
        if (isSocketAndShouldSnap)
        {
            SnapToTarget(appliedStopDistance);
            return;
        }

        float appliedDistanceToSnap = (targetPosition == originalPosition ? returnSnapDistance : playerStopDistance) + 0.001f;
        bool isNotSocketAndShouldSnap = targetSocket == null && (nextDistance <= appliedDistanceToSnap || currentDistance <= appliedDistanceToSnap);
        if (isNotSocketAndShouldSnap)
        {
            SnapToTarget(appliedStopDistance);
            return;
        }

        Vector2 direction = translation.normalized;
        float moveDistance = translation.magnitude;

        RaycastHit2D hit = Physics2D.BoxCast(
            mainCollider.bounds.center,
            mainCollider.bounds.size,
            0f,
            direction,
            moveDistance,
            wallLayer
        );

        if (hit.collider == null || isReturning)
        {
            transform.Translate(translation);
            ValidatePosition();
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
        if (isReturning)
        {
            transform.position = originalPosition;
            spriteTransform.rotation = Quaternion.identity;
        }
        else
        {
            transform.position = targetPosition + (directionFromPlayerToPlug * appliedStopDistance);
            if (targetSocket)
            {
                spriteTransform.rotation = Quaternion.Euler(targetSocket.connectionRotation);
            }
        }
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
        foreach (var collider in allColliders)
        {
            collider.excludeLayers = 0;
        }
        if (targetSocket) targetSocket.ChangeActivationState(true);
        plugSoundPlayer.PlayImpactSFX();
        ValidatePosition();
    }

    void ValidatePosition()
    {
        if ((Vector2)transform.position == originalPosition) isInContainer = true;
        else isInContainer = false;
    }
    #endregion
}