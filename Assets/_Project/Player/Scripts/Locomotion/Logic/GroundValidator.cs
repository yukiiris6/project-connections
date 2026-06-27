using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Player
{
    public class GroundValidator : MonoBehaviour
    {
        [SerializeField, Required] BoxCollider2D feetCollider;
        [SerializeField, Required] LayerMask groundLayers;
        [SerializeField] Rigidbody2D myRigidbody;
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
            RaycastHit2D hit = Physics2D.BoxCast(
                feetCollider.bounds.center,
                feetCollider.bounds.size,
                0f,
                Vector2.down,
                groundCheckDistance,
                groundLayers
            );

            bool isAboveFloor = hit.collider != null && hit.normal.y > 0.7f;
            if (isAboveFloor && IsGrounded == false && myRigidbody.linearVelocityY <= 0)
            {
                Land();
                return;
            }

            coyoteTimeCounter += Time.fixedDeltaTime;
            if (!isAboveFloor && coyoteTimeCounter > coyoteTimeDuration && IsGrounded == true)
            {
                ExitGround();
            }
        }

        void Land()
        {
            IsGrounded = true;
            coyoteTimeCounter = 0;
            OnLand?.Invoke();
        }

        void ExitGround()
        {
            IsGrounded = false;
            OnExitGround?.Invoke();
        }
    }
}
