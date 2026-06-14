using UnityEngine;

public class PlayerAnimationBrain : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerAnimatorHandler animationHandler;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] PlayerOrientation playerOrientation;
    [SerializeField] GroundValidator groundValidator;
    [SerializeField] Jumper jumper;
    [SerializeField] MagnetAiming magnetAiming;

    void OnEnable()
    {
        playerController.OnAimInput += UpdateAiming;
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
        jumper.OnYVelocityUpdated -= UpdateJumpVelocity;
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
        animationHandler.UpdateAiming(isAiming);
    }

    void UpdateAimingFacing(float xDirection)
    {
        playerOrientation.SetFacing(xDirection);
    }
}
