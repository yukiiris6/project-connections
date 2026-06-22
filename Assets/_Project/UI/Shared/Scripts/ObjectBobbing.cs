using UnityEngine;
using Sirenix.OdinInspector;
using DG.Tweening;

namespace ProjectConnections.UIShared
{
    public class ObjectBobbing : MonoBehaviour
    {
        [SerializeField, Required] RectTransform rectTransform;

        [Header("Bobbing")]
        [Tooltip("Will use minimum value if false")]
        [SerializeField, Required] bool shouldRandomizeBobbing;
        [SerializeField, Required] float bobAmountMin = 2f;
        [SerializeField, Required] float bobAmountMax = 6f;

        [Header("Duration")]
        [Tooltip("Will use minimum value if false")]
        [SerializeField, Required] bool shouldRandomizeDuration;
        [SerializeField, Required] float durationMin = 1f;
        [SerializeField, Required] float durationMax = 2f;

        [Header("Misc")]
        [SerializeField, Required] bool shouldHaveStartDelay;
        [SerializeField, Required] Ease easeType = Ease.InOutSine;


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
}
