using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Reference")]
        [SerializeField, Required] Rigidbody2D myRigidbody;
        [SerializeField, Required] PlayerController playerController;

        [Header("Movement")]
        [field: SerializeField] public float MaxVelocity = 2f;
        [SerializeField, Required] float acceleration = 1f;
        [SerializeField, Required] float deceleration = 1f;
        [SerializeField, Required] float velocityStopOffset = .5f;

        public event Action<Vector2, float> OnMovement;

        public bool LastFacedLeft = false;
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
            if (newInput.x != 0)
            {
                LastFacedLeft = newInput.x < 0;
            }
        }

        void Move()
        {
            float newXVelocity = GetNewVelocity();
            float clampedVelocity = Mathf.Clamp(newXVelocity, -MaxVelocity, MaxVelocity);

            Vector2 playerPosition = transform.position;

            myRigidbody.linearVelocityX = clampedVelocity;
            OnMovement?.Invoke(moveInput, clampedVelocity);
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

            float velocityToSubtract = deceleration * Time.fixedDeltaTime;
            return Mathf.MoveTowards(playerXVelocity, 0f, velocityToSubtract);
        }
    }
}
