using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(AudioSource))]
public class SquareIrisWipeController : MonoBehaviour
{
    [SerializeField] Material irisMaterial;
    [SerializeField] AudioClip transitionSFX;
    [SerializeField] float duration = 1.5f;

    public float Duration => duration;

    AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        irisMaterial.SetFloat("_Radius", 1f);
    }

    public void StartIrisOpen()
    {
        float percentageTrigger = .5f * .2f;
        float nextTargetRadius = percentageTrigger;

        irisMaterial.SetFloat("_Radius", 0);
        irisMaterial.DOFloat(.5f, "_Radius", duration)
            .SetUpdate(true)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                float currentRadius = irisMaterial.GetFloat("_Radius");
                if (currentRadius >= nextTargetRadius)
                {
                    audioSource.PlayOneShot(transitionSFX);
                    nextTargetRadius += percentageTrigger;
                }
            });
    }

    public void StartIrisWipe()
    {
        float percentageTrigger = .5f * .2f;
        float nextTargetRadius = .49f;

        irisMaterial.SetFloat("_Radius", .5f);
        irisMaterial.DOFloat(0f, "_Radius", duration)
            .SetUpdate(true)
            .SetEase(Ease.Linear)
            .OnUpdate(() =>
            {
                float currentRadius = irisMaterial.GetFloat("_Radius");
                if (currentRadius <= nextTargetRadius)
                {
                    audioSource.PlayOneShot(transitionSFX);
                    nextTargetRadius -= percentageTrigger;
                }
            });
    }
}