using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Rendering.Universal;

namespace ProjectConnections.Electric
{
    public class DoorPresenter : MonoBehaviour
    {
        [Header("Animation")]
        [SerializeField, Required] Animator doorAnimator;
        [SerializeField, Required] string IsOpenBool = "IsOpen";

        [Header("Light")]
        [SerializeField, Required] ElectricObjectLight electricObjectLight;

        public void UpdateState(bool hasEnergy)
        {
            doorAnimator.SetBool(IsOpenBool, hasEnergy);
            electricObjectLight.UpdateState(hasEnergy);
        }
    }
}
