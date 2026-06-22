using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Rendering.Universal;

namespace ProjectConnections.Electric
{
    public class ElevatorDockPresenter : MonoBehaviour
    {
        [Header("Sprite")]
        [SerializeField, Required] SpriteRenderer spriteRenderer;
        [SerializeField, Required] Sprite spriteOn;
        [SerializeField, Required] Sprite spriteOff;

        [Header("Light")]
        [SerializeField, Required] ElectricObjectLight electricObjectLight;

        public void UpdateState(bool hasEnergy)
        {
            spriteRenderer.sprite = hasEnergy ? spriteOn : spriteOff;
            electricObjectLight.UpdateState(hasEnergy);
        }
    }
}
