using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace ProjectConnections.Electric
{
    public class ElevatorDockPresenter : MonoBehaviour
    {
        [Header("Sprite")]
        [SerializeField] SpriteRenderer spriteRenderer;
        [SerializeField] Sprite spriteOn;
        [SerializeField] Sprite spriteOff;

        [Header("Light")]
        [SerializeField] ElectricObjectLight electricObjectLight;

        public void UpdateState(bool hasEnergy)
        {
            spriteRenderer.sprite = hasEnergy ? spriteOn : spriteOff;
            electricObjectLight.UpdateState(hasEnergy);
        }
    }
}