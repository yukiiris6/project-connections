using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Player
{
    public class PlayerAnimationBrain : MonoBehaviour
    {
        [SerializeField, Required] PlayerAnimatorHandler animationHandler;
        [SerializeField, Required] PlayerMovement playerMovement;
        [SerializeField, Required] PlayerOrientation playerOrientation;
        [SerializeField, Required] GroundValidator groundValidator;
        [SerializeField, Required] Jumper jumper;
        [SerializeField, Required] MagnetAiming magnetAiming;

        void OnEnable()
        {
            playerMovement.OnMovement += UpdateRunningAnimation;
            groundValidator.OnLand += UpdateGroundedOnLand;
            groundValidator.OnExitGround += UpdateGroundedOnExitGround;
            jumper.OnYVelocityUpdated += UpdateJumpVelocity;
            magnetAiming.OnChangeFacing += UpdateAimingFacing;
        }

        void OnDisable()
        {
            playerMovement.OnMovement -= UpdateRunningAnimation;
            groundValidator.OnLand -= UpdateGroundedOnLand;
            groundValidator.OnExitGround -= UpdateGroundedOnExitGround;
            magnetAiming.OnChangeFacing -= UpdateAimingFacing;
        }

        void UpdateRunningAnimation(Vector2 moveInput, float xVelocity)
        {
            float maxVelocity = playerMovement.MaxVelocity;
            bool isRunning = Mathf.Abs(xVelocity) > 0;
            animationHandler.UpdateRunning(isRunning);
            animationHandler.UpdateMovementSpeed(xVelocity, maxVelocity);
            playerOrientation.SetFacing(xVelocity);
        }

        void UpdateJumpVelocity(float yVelocity)
        {
            animationHandler.UpdateYVelocity(yVelocity);
        }

        void UpdateGroundedOnLand()
        {
            animationHandler.UpdateGrounded(true);
        }

        void UpdateGroundedOnExitGround()
        {
            animationHandler.UpdateGrounded(false);
        }

        void UpdateAimingFacing(float xDirection)
        {
            playerOrientation.SetFacing(xDirection);
        }
    }
}
