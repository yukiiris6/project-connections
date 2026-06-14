using UnityEngine;
using DG.Tweening;

public class ObjectBobbing : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;

    [Header("Bobbing")]
    [Tooltip("Will use minimum value if false")]
    [SerializeField] bool shouldRandomizeBobbing;
    [SerializeField] float bobAmountMin = 2f;
    [SerializeField] float bobAmountMax = 6f;

    [Header("Duration")]
    [Tooltip("Will use minimum value if false")]
    [SerializeField] bool shouldRandomizeDuration;
    [SerializeField] float durationMin = 1f;
    [SerializeField] float durationMax = 2f;

    [Header("Misc")]
    [SerializeField] bool shouldHaveStartDelay;
    [SerializeField] Ease easeType = Ease.InOutSine;


    void GetDependencies()
    {
        if (rectTransform != null) return;
        rectTransform = GetComponent<RectTransform>();
    }

    void Awake() => GetDependencies();

    void Start()
    {
        GetDependencies();

        Canvas.ForceUpdateCanvases();

        float delay = 0;

        var bobAmount = bobAmountMin;
        var duration = durationMin;

        if (shouldHaveStartDelay) delay = Random.Range(0f, durationMin);
        if (shouldRandomizeDuration) duration = Random.Range(durationMin, durationMax);
        if (shouldRandomizeBobbing) bobAmount = Random.Range(bobAmountMin, bobAmountMax);

        rectTransform.DOAnchorPosY(rectTransform.anchoredPosition.y + bobAmount, duration)
            .SetEase(easeType)
            .SetLoops(-1, LoopType.Yoyo)
            .SetDelay(delay);
    }
}
