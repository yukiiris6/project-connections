using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

[RequireComponent(typeof(PlayerAnimator))]
public class PlayerJumping : MonoBehaviour
{
    [Header("Jumping")]
    [SerializeField] float jumpStrength = 1f;
    [SerializeField] float minJumpTime = 1f;
    [SerializeField] float jumpCutGravity = 5f;
    [SerializeField] float jumpApexThreshold = .1f;
    [SerializeField] float coyoteTimeDuration = .1f;

    [Header("Ground Detection")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float widthReduction = .1f;
    [SerializeField] float groundCheckDistance = .05f;

    BoxCollider2D myCollider;
    Rigidbody2D myRigidBody;
    PlayerAnimator playerAnimator;
    PlayerProgress playerProgress;

    float originalGravity;
    bool isGrounded = true;
    bool canJump = false;
    bool isJumping = false;
    bool isHoldingJump = false;
    float coyoteTimeCounter = 0f;
    float jumpTimeCounter = 0f;

    #region Unity Lifecycle
    void Awake()
    {
        myCollider = GetComponent<BoxCollider2D>();
        myRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<PlayerAnimator>();
        playerProgress = GetComponent<PlayerProgress>();
        originalGravity = myRigidBody.gravityScale;
    }

    void FixedUpdate()
    {
        GroundedHandler();
        MidJumpHandler();
        PostJumpHandler();
        JumpAnimationHandler();
    }
    #endregion

    #region Jumping
    void OnJump(InputValue value)
    {
        if (playerProgress.HasFinished) return;
        if (value.isPressed) PerformJump();
        isHoldingJump = value.isPressed;
    }

    void PerformJump()
    {
        if (!canJump) return;
        jumpTimeCounter = 0;
        myRigidBody.linearVelocityY = jumpStrength;
        isJumping = true;
    }

    void StopJump()
    {
        if (myRigidBody.linearVelocityY > 0)
        {
            myRigidBody.gravityScale = originalGravity + jumpCutGravity;
        }
    }

    void RestoreGravity()
    {
        myRigidBody.gravityScale = originalGravity;
    }
    #endregion

    #region Validation
    void GroundedHandler()
    {
        if (myCollider == null) return;

        Vector3 checkSize = new(myCollider.bounds.size.x - widthReduction, myCollider.bounds.size.y);
        RaycastHit2D hit = Physics2D.BoxCast(
            myCollider.bounds.center,
            checkSize,
            0f,
            Vector2.down,
            groundCheckDistance,
            groundLayer
        );

        if (hit.collider == null)
        {
            isGrounded = false;
            if (coyoteTimeCounter > coyoteTimeDuration)
            {
                canJump = false;
                return;
            }
            coyoteTimeCounter += Time.fixedDeltaTime;
        }
        else
        {
            isGrounded = true;
            canJump = true;
            coyoteTimeCounter = 0;
        }
    }

    void MidJumpHandler()
    {
        if (!isJumping) return;
        if (jumpTimeCounter > minJumpTime && !isHoldingJump) StopJump();
        else jumpTimeCounter += Time.fixedDeltaTime;
    }

    void PostJumpHandler()
    {
        if (!isJumping) return;
        if (jumpTimeCounter <= minJumpTime) return;

        bool isFalling = myRigidBody.linearVelocityY < 0;
        bool isOnJumpsApex = Mathf.Abs(myRigidBody.linearVelocityY) < jumpApexThreshold;
        if (isFalling || isOnJumpsApex)
        {
            isJumping = false;
            RestoreGravity();
        }
    }

    void JumpAnimationHandler()
    {
        playerAnimator.ControlJumpingAnimation(myRigidBody.linearVelocityY, isGrounded);
    }
    #endregion
}
