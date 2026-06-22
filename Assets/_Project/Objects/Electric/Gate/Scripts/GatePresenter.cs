using System;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Rendering.Universal;

namespace ProjectConnections.Electric
{
    public class GatePresenter : MonoBehaviour
    {
        [Header("Sprite")]
        [SerializeField, Required] SpriteRenderer[] spriteRenderers;
        [SerializeField, Required] Sprite onSprite;
        [SerializeField, Required] Sprite offSprite;

        [Header("Light")]
        [SerializeField, Required] ElectricObjectLight electricObjectLight;

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
