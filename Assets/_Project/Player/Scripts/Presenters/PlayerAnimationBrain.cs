using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Player
{
    public class PlayerAnimationBrain : MonoBehaviour
    {
        [SerializeField, Required] PlayerController playerController;
        [SerializeField, Required] PlayerAnimatorHandler animationHandler;
        [SerializeField, Required] PlayerMovement playerMovement;
        [SerializeField, Required] PlayerOrientation playerOrientation;
        [SerializeField, Required] GroundValidator groundValidator;
        [SerializeField, Required] Jumper jumper;
        [SerializeField, Required] MagnetAiming magnetAiming;
        [SerializeField, Required] Carrier carrier;

        void OnEnable()
        {
            playerController.OnAimInput += UpdateAiming;
            playerMovement.OnMovement += UpdateRunningAnimation;
            groundValidator.OnLand += UpdateGroundedOnLand;
            groundValidator.OnExitGround += UpdateGroundedOnExitGround;
            jumper.OnYVelocityUpdated += UpdateJumpVelocity;
            magnetAiming.OnChangeFacing += UpdateAimingFacing;
            carrier.OnCarryChanged += HandleCarryAnimation;
        }

        void OnDisable()
        {
            playerController.OnAimInput -= UpdateAiming;
            playerMovement.OnMovement -= UpdateRunningAnimation;
            groundValidator.OnLand -= UpdateGroundedOnLand;
            groundValidator.OnExitGround -= UpdateGroundedOnExitGround;
            jumper.OnYVelocityUpdated -= UpdateJumpVelocity;
            magnetAiming.OnChangeFacing -= UpdateAimingFacing;
            carrier.OnCarryChanged -= HandleCarryAnimation;
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

        void UpdateAiming(bool isAiming)
        {
            animationHandler.UpdateCarrying(false);
            animationHandler.UpdateAiming(isAiming);
            animationHandler.ToggleArms(!isAiming);
        }

        void UpdateAimingFacing(float xDirection)
        {
            playerOrientation.SetFacing(xDirection);
        }

        void HandleCarryAnimation(bool isCarrying)
        {
            animationHandler.UpdateAiming(false);
            animationHandler.UpdateCarrying(isCarrying);
            animationHandler.ToggleArms(!isCarrying);
        }
    }
}
