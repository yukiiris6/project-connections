using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerProgress))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float acceleration = 1f;
    [SerializeField] float deceleration = 1f;
    [SerializeField] float maxVelocity = 2f;
    [SerializeField] float velocityStopOffset = .5f;

    Vector2 moveInput;
    Rigidbody2D myRigidBody;
    PlayerAnimator playerAnimator;
    PlayerProgress playerProgress;

    #region Unity Lifecycle
    void Awake()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<PlayerAnimator>();
        playerProgress = GetComponent<PlayerProgress>();
    }

    void FixedUpdate()
    {
        Move();
    }
    #endregion

    #region Input Handling
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }
    #endregion

    #region Movement
    void Move()
    {
        if (playerProgress.HasFinished) return;

        float newXVelocity;
        if (moveInput.x == 0) newXVelocity = GetDecelerationVelocity();
        else newXVelocity = GetAccelerateVelocity();

        myRigidBody.linearVelocityX = Mathf.Clamp(newXVelocity, -maxVelocity, maxVelocity);
        playerAnimator.ControlRunningAnimation(moveInput.x, newXVelocity, maxVelocity);
    }

    float GetAccelerateVelocity()
    {
        float velocityToAdd = moveInput.x * acceleration * Time.fixedDeltaTime;
        return myRigidBody.linearVelocityX + velocityToAdd;
    }

    float GetDecelerationVelocity()
    {
        float playerXVelocity = myRigidBody.linearVelocityX;
        if (Math.Abs(playerXVelocity) < velocityStopOffset)
        {
            return 0;
        }
        else
        {
            float directionSign = Mathf.Sign(playerXVelocity);
            float velocityToSubtract = directionSign * deceleration * Time.fixedDeltaTime;
            return playerXVelocity - velocityToSubtract;
        }
    }
    #endregion
}
