using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.UI.Overlay
{
    public class BlackScreenTransition : MonoBehaviour
    {
        [field: SerializeField] public float FadeDuration { get; private set; } = 1f;
        [SerializeField, Required] CanvasGroup blackImageCanvasGroup;

        public void FadeIn()
        {
            blackImageCanvasGroup.DOFade(0, FadeDuration).SetUpdate(true).SetEase(Ease.InCubic);
        }

        public void FadeOut()
        {
            blackImageCanvasGroup.DOFade(1, FadeDuration).SetUpdate(true).SetEase(Ease.OutCubic);
        }
    }
}
