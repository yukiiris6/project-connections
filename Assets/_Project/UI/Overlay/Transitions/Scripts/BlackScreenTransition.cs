using DG.Tweening;
using UnityEngine;

public class BlackScreenTransition : MonoBehaviour
{
    [field: SerializeField] public float FadeDuration { get; private set; } = 1f;
    [SerializeField] CanvasGroup blackImageCanvasGroup;

    public void FadeIn()
    {
        blackImageCanvasGroup.DOFade(1, FadeDuration).SetUpdate(true).SetEase(Ease.InCubic);
    }

    public void FadeOut()
    {
        blackImageCanvasGroup.DOFade(0, FadeDuration).SetUpdate(true).SetEase(Ease.OutCubic);
    }
}