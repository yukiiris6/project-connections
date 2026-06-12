using UnityEngine;

public class PlayerAnimatorHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] Animator bodyAnimator;
    [SerializeField] Animator aimingArmsAnimator;
    [SerializeField] GameObject armsObject;

    static readonly int yVelocityHash = Animator.StringToHash("yVelocity");
    static readonly int movementSpeedHash = Animator.StringToHash("movementSpeed");
    static readonly int isFacingLeftHash = Animator.StringToHash("IsFacingLeft");
    static readonly int isRunningHash = Animator.StringToHash("IsRunning");
    static readonly int isGroundedHash = Animator.StringToHash("IsGrounded");
    static readonly int isAimingHash = Animator.StringToHash("IsAiming");
    static readonly int hasFinishedHash = Animator.StringToHash("HasFinished");

    public void UpdateFinished(bool hasFinished)
    {
        bodyAnimator.SetBool(hasFinishedHash, hasFinished);
    }

    public void UpdateRunning(bool isRunning)
    {
        bodyAnimator.SetBool(isRunningHash, isRunning);
    }

    public void UpdateMovementSpeed(float value, float maxVelocity)
    {
        float movementSpeed = Mathf.Clamp(Mathf.Abs(value) / maxVelocity, 0, 1);
        bodyAnimator.SetFloat(movementSpeedHash, movementSpeed);
    }

    public void UpdateFacing(bool isFacingLeft)
    {
        bodyAnimator.SetBool(isFacingLeftHash, isFacingLeft);
        aimingArmsAnimator.SetBool(isFacingLeftHash, isFacingLeft);
    }

    public void UpdateYVelocity(float newYVelocity)
    {
        bodyAnimator.SetFloat(yVelocityHash, newYVelocity);
    }

    public void UpdateGrounded(bool isGrounded)
    {
        bodyAnimator.SetBool(isGroundedHash, isGrounded);
    }

    public void UpdateAiming(bool showAimingArms)
    {
        aimingArmsAnimator.SetBool(isAimingHash, showAimingArms);
        armsObject.SetActive(!showAimingArms);
    }
}
