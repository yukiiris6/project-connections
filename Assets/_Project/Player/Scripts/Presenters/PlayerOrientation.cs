using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerOrientation : MonoBehaviour
{
    [SerializeField, Required] PlayerMovement playerMovement;
    [SerializeField, Required] PlayerAnimatorHandler playerAnimation;


    public void SetFacing(float x)
    {
        if (x == 0) return;
        bool isFacingLeft = x < 0;
        playerAnimation.UpdateFacing(isFacingLeft);
    }
}