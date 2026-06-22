using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Player
{
    public class Jumper : MonoBehaviour
    {
        [Header("References")]
        [SerializeField, Required] Rigidbody2D myRigidBody;
        [SerializeField, Required] GroundValidator groundValidator;

        [Header("Config")]
        [SerializeField, Required] float jumpStrength = 1f;
        [SerializeField, Required] float maxJumpTime = 1f;
        [SerializeField, Required] float cutGravityStrength = 5f;
        [SerializeField, Required] float jumpApexThreshold = .1f;

        public event Action OnFall;
        public event Action<float> OnYVelocityUpdated;

        float lastYVelocity = 0f;
        float airTimeCounter = 0f;
        float originalGravity;

        void Awake()
        {
            originalGravity = myRigidBody.gravityScale;
        }

        void OnEnable()
        {
            groundValidator.OnLand += HandleLand;
        }

        void OnDisable()
        {
            groundValidator.OnLand -= HandleLand;
        }

        void FixedUpdate()
        {
            PostJumpHandler();
            VerifyVelocity();
        }

        void HandleLand()
        {
            RestoreGravity();
        }

        public void Jump()
        {
            myRigidBody.linearVelocityY = jumpStrength;
            airTimeCounter = 0;
        }

        public void StopJump()
        {
            if (myRigidBody.linearVelocityY > 0)
            {
                CutGravity();
            }
        }

        void VerifyVelocity()
        {
            if (lastYVelocity != myRigidBody.linearVelocityY)
            {
                OnYVelocityUpdated?.Invoke(myRigidBody.linearVelocityY);
            }
            lastYVelocity = myRigidBody.linearVelocityY;
        }

        void PostJumpHandler()
        {
            bool isFalling = myRigidBody.linearVelocityY < 0;
            bool isOnJumpsApex = Mathf.Abs(myRigidBody.linearVelocityY) < jumpApexThreshold;
            if (isFalling || isOnJumpsApex)
            {
                RestoreGravity();
                OnFall?.Invoke();
            }
        }

        void CutGravity()
        {
            airTimeCounter = maxJumpTime;
            myRigidBody.gravityScale = originalGravity + cutGravityStrength;
        }

        void RestoreGravity()
        {
            myRigidBody.gravityScale = originalGravity;
        }
    }
}
