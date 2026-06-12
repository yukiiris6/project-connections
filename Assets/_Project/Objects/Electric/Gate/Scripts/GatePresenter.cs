using System;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace ProjectConnections.Electric
{
    public class GatePresenter : MonoBehaviour
    {
        [Header("Sprite")]
        [SerializeField] SpriteRenderer[] spriteRenderers;
        [SerializeField] Sprite onSprite;
        [SerializeField] Sprite offSprite;

        [Header("Light")]
        [SerializeField] ElectricObjectLight electricObjectLight;

        public void UpdateStatus(bool hasEnergy)
        {
            electricObjectLight.UpdateState(hasEnergy);
            Sprite newSprite = hasEnergy ? onSprite : offSprite;
            Array.ForEach(
                spriteRenderers,
                spriteRenderer => spriteRenderer.sprite = newSprite
            );
        }
    }
}
