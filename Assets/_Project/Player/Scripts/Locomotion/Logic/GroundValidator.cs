using System;
using UnityEngine;
using Sirenix.OdinInspector;

public class GroundValidator : MonoBehaviour
{
    [SerializeField, Required] BoxCollider2D feetCollider;
    [SerializeField, Required] LayerMask groundLayers;
    [SerializeField, Required] float groundCheckDistance = .05f;
    [SerializeField, Required] float coyoteTimeDuration = .1f;

    public event Action OnLand;
    public event Action OnExitGround;

    public bool IsGrounded { get; private set; }
    float coyoteTimeCounter = 0f;

    void Update()
    {
        GroundCheck();
    }

    void GroundCheck()
    {
        Vector3 checkSize = new(feetCollider.bounds.size.x, feetCollider.bounds.size.y);
        RaycastHit2D hit = Physics2D.BoxCast(
            feetCollider.bounds.center,
            checkSize,
            0f,
            Vector2.down,
            groundCheckDistance,
            groundLayers
        );

        if (hit.collider == null)
        {
            if (coyoteTimeCounter > coyoteTimeDuration)
            {
                IsGrounded = false;
                OnExitGround?.Invoke();
                return;
            }
            else
            {
                coyoteTimeCounter += Time.fixedDeltaTime;
            }
        }
        else if (IsGrounded == false)
        {
            IsGrounded = true;
            coyoteTimeCounter = 0;
            OnLand?.Invoke();
        }
    }
}