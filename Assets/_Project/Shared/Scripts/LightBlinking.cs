using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Rendering.Universal;

public class LightBlinking : MonoBehaviour
{
    [SerializeField, Required] Light2D light2D;
    [SerializeField, Required] float blinkingCooldownMin = .25f;
    [SerializeField, Required] float blinkingCooldownMax = .35f;
    [SerializeField, Required] float blinkThreshold = .01f;
    [SerializeField, Required] float lowerIntensity = .5f;
    [SerializeField, Required] float maxIntensity = 1f;

    float blinkingTimer = 0f;
    float appliedCooldown = .25f;

    void Update()
    {
        Blink();
    }

    void Blink()
    {
        float result = Random.Range(0, 1f);

        if (result < blinkThreshold && blinkingTimer >= appliedCooldown)
        {
            light2D.intensity = lowerIntensity;
            blinkingTimer = 0;
            appliedCooldown = Random.Range(blinkingCooldownMin, blinkingCooldownMax);
        }
        else if (blinkingTimer >= appliedCooldown)
        {
            light2D.intensity = maxIntensity;
        }

        blinkingTimer += Time.deltaTime;
    }
}