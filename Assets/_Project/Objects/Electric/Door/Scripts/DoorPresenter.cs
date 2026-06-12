using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace ProjectConnections.Electric
{
    public class DoorPresenter : MonoBehaviour
    {
        [Header("Animation")]
        [SerializeField] Animator doorAnimator;
        [SerializeField] string IsOpenBool = "IsOpen";

        [Header("Light")]
        [SerializeField] ElectricObjectLight electricObjectLight;

        public void UpdateState(bool hasEnergy)
        {
            doorAnimator.SetBool(IsOpenBool, hasEnergy);
            electricObjectLight.UpdateState(hasEnergy);
        }
    }
}
