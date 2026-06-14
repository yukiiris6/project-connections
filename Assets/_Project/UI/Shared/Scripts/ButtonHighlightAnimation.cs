using DG.Tweening;
using UnityEngine;

public class ButtonHighlightAnimation : MonoBehaviour
{
    [SerializeField] GameObject buttonHighlightObject;

    Tween tween;

    public void ShowHighlight()
    {
        KillTween();
        buttonHighlightObject.SetActive(true);
        buttonHighlightObject.transform.position = transform.position;
        buttonHighlightObject.transform.localScale = new(1f, 1f, 1f);
        tween = buttonHighlightObject.transform.DOScale(1.2f, .5f)
            .SetUpdate(true)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    public void HideHighlight()
    {
        KillTween();
        buttonHighlightObject.SetActive(false);
    }

    void KillTween()
    {
        if (tween != null)
        {
            tween.Kill();
            tween = null;
        }
    }
}