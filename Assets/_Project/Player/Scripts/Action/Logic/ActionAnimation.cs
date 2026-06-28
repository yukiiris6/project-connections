using UnityEngine;

namespace ProjectConnections.Player
{
    public class ActionAnimation : MonoBehaviour
    {
        [SerializeField] PlayerAnimatorHandler animationHandler;

        public void UpdateMagnetizeAnimation(bool isAiming)
        {
            animationHandler.UpdateCarrying(false);
            animationHandler.UpdateAiming(isAiming);
            animationHandler.ToggleArms(!isAiming);
        }

        public void UpdateCarryingAnimation(bool isCarrying)
        {
            animationHandler.UpdateAiming(false);
            animationHandler.UpdateCarrying(isCarrying);
            animationHandler.ToggleArms(!isCarrying);
        }
    }
}
