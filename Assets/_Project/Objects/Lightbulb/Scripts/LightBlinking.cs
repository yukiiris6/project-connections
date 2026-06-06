using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(Light2D))]
public class LightBlinking : MonoBehaviour
{
    [SerializeField] float blinkingCooldownMin = .25f;
    [SerializeField] float blinkingCooldownMax = .35f;
    [SerializeField] float blinkThreshold = .01f;
    [SerializeField] float lowerIntensity = .5f;
    [SerializeField] float maxIntensity = 1f;

    Light2D light2D;

    float blinkingTimer = 0f;
    float appliedCooldown = .25f;

    void Start()
    {
        light2D = GetComponent<Light2D>();
    }

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
