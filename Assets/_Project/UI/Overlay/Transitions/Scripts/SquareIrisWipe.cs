using DG.Tweening;
using UnityEngine;

public class SquareIrisWipe : MonoBehaviour
{
    [field: SerializeField] public float Duration { get; private set; } = 1.5f;
    [SerializeField] Material irisMaterial;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip transitionSFX;

    void Start()
    {
        irisMaterial.SetFloat("_Radius", 1f);
    }

    public void PlayIrisOpen()
    {
        float percentageTrigger = .5f * .2f;
        float nextTargetRadius = percentageTrigger;

        irisMaterial.SetFloat("_Radius", 0);
        irisMaterial.DOFloat(.5f, "_Radius", Duration)
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

    public void PlayIrisWipe()
    {
        float percentageTrigger = .5f * .2f;
        float nextTargetRadius = .49f;

        irisMaterial.SetFloat("_Radius", .5f);
        irisMaterial.DOFloat(0f, "_Radius", Duration)
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

    public void ResetIris()
    {
        irisMaterial.SetFloat("_Radius", 5f);
    }
}