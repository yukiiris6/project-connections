using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] Rigidbody2D myRigidbody;
    [SerializeField] PlayerController playerController;

    [Header("Movement")]
    [field: SerializeField] public float MaxVelocity = 2f;
    [SerializeField] float acceleration = 1f;
    [SerializeField] float deceleration = 1f;
    [SerializeField] float velocityStopOffset = .5f;

    public event Action<Vector2, float> OnMovement;

    Vector2 moveInput;

    void FixedUpdate()
    {
        Move();
    }

    void OnEnable()
    {
        playerController.OnMoveInput += SetMoveInput;
    }

    void OnDisable()
    {
        playerController.OnMoveInput -= SetMoveInput;
    }

    public void Stop()
    {
        myRigidbody.linearVelocityX = 0;
        moveInput = Vector2.zero;
    }

    void SetMoveInput(Vector2 newInput)
    {
        moveInput = newInput;
    }

    void Move()
    {
        float newXVelocity = GetNewVelocity();
        myRigidbody.linearVelocityX = Mathf.Clamp(newXVelocity, -MaxVelocity, MaxVelocity);
        OnMovement?.Invoke(moveInput, newXVelocity);
    }

    float GetNewVelocity()
    {
        if (moveInput.x == 0) return GetDecreasedVelocity();
        else return GetIncreasedVelocity();
    }

    float GetIncreasedVelocity()
    {
        float velocityToAdd = moveInput.x * acceleration * Time.fixedDeltaTime;
        return myRigidbody.linearVelocityX + velocityToAdd;
    }

    float GetDecreasedVelocity()
    {
        float playerXVelocity = myRigidbody.linearVelocityX;

        if (Math.Abs(playerXVelocity) < velocityStopOffset) return 0;

        float directionSign = Mathf.Sign(playerXVelocity);
        float velocityToSubtract = directionSign * deceleration * Time.fixedDeltaTime;
        return playerXVelocity - velocityToSubtract;
    }
}
