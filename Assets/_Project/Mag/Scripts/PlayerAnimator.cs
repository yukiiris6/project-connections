using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("References")]
    [SerializeField] Animator bodyAnimator;
    [SerializeField] Animator armsAnimator;
    [SerializeField] Animator backArmsAnimator;

    [Header("Strings")]
    [SerializeField] string yVelocityString = "yVelocity";
    [SerializeField] string movementSpeedString = "movementSpeed";
    [SerializeField] string isFacingLeftString = "IsFacingLeft";
    [SerializeField] string isRunningString = "IsRunning";
    [SerializeField] string isGroundedString = "IsGrounded";
    [SerializeField] string isAimingString = "IsAiming";
    [SerializeField] string hasFinishedString = "HasFinished";

    [Header("Magnet Anchors")]
    [SerializeField] Transform leftMagnetAnchor;
    [SerializeField] Transform rightMagnetAnchor;

    float leftMagnetY;
    float rightMagnetY;

    void Awake()
    {
        leftMagnetY = leftMagnetAnchor.localPosition.y;
        rightMagnetY = rightMagnetAnchor.localPosition.y;
    }

    public void ControlRunningAnimation(float lastMoveInput, float xVelocity, float maxVelocity)
    {
        if (lastMoveInput != 0)
        {
            bool isFacingLeft = lastMoveInput < 0;
            SetIsFacingLeft(isFacingLeft);
        }
        if (Mathf.Abs(xVelocity) <= 0)
        {
            SetIsRunning(false);
        }
        else
        {
            SetIsRunning(true);
            UpdateMovementSpeed(xVelocity, maxVelocity);
        }
    }

    public void ControlJumpingAnimation(float yVelocity, bool isGrounded)
    {
        SetYVelocity(yVelocity);
        SetIsGrounded(isGrounded);
    }

    public void ControlAimingAnimation(bool isAiming, bool isFacingLeft)
    {
        SetIsAiming(isAiming);
        SetIsFacingLeft(isFacingLeft);
    }

    public void PlayFinishAnimation()
    {
        bodyAnimator.SetBool(hasFinishedString, true);
        armsAnimator.SetBool(hasFinishedString, true);
        SetIsAiming(false);
    }

    void UpdateMovementSpeed(float value, float maxVelocity)
    {
        float movementSpeed = Mathf.Clamp(Mathf.Abs(value) / maxVelocity, 0, 1);
        bodyAnimator.SetFloat(movementSpeedString, movementSpeed);
        armsAnimator.SetFloat(movementSpeedString, movementSpeed);
    }

    void SetIsRunning(bool value)
    {
        bodyAnimator.SetBool(isRunningString, value);
        armsAnimator.SetBool(isRunningString, value);
    }

    void SetIsFacingLeft(bool value)
    {
        bodyAnimator.SetBool(isFacingLeftString, value);
        armsAnimator.SetBool(isFacingLeftString, value);
        backArmsAnimator.SetBool(isFacingLeftString, value);
        if (value)
        {
            leftMagnetAnchor.localPosition = new Vector3(leftMagnetAnchor.localPosition.x, -leftMagnetY, 0f);
            rightMagnetAnchor.localPosition = new Vector3(leftMagnetAnchor.localPosition.x, -rightMagnetY, 0f);
        }
        else
        {
            leftMagnetAnchor.localPosition = new Vector3(leftMagnetAnchor.localPosition.x, leftMagnetY, 0f);
            rightMagnetAnchor.localPosition = new Vector3(leftMagnetAnchor.localPosition.x, rightMagnetY, 0f);
        }
    }

    void SetYVelocity(float value)
    {
        bodyAnimator.SetFloat(yVelocityString, value);
        armsAnimator.SetFloat(yVelocityString, value);
    }

    void SetIsGrounded(bool value)
    {
        bodyAnimator.SetBool(isGroundedString, value);
        armsAnimator.SetBool(isGroundedString, value);
    }

    public void SetIsAiming(bool value)
    {
        armsAnimator.SetBool(isAimingString, value);
        backArmsAnimator.SetBool(isAimingString, value);
    }
}
