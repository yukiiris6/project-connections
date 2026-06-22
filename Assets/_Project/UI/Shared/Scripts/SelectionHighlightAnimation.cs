using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.UIShared
{
    public class SelectionHighlightAnimation : MonoBehaviour
    {
        [SerializeField, Required] Transform buttonHighlightTransform;
        [SerializeField, Required] float maxScale = 1.2f;
        [SerializeField, Required] float scaleDuration = .5f;

        Tween tween;

        public void ShowHighlightAt(Vector2 position)
        {
            KillTween();
            buttonHighlightTransform.gameObject.SetActive(true);
            buttonHighlightTransform.position = position;
            buttonHighlightTransform.localScale = new(1f, 1f, 1f);
            tween = buttonHighlightTransform.DOScale(maxScale, scaleDuration)
                .SetUpdate(true)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }

        public void HideHighlight()
        {
            KillTween();
            buttonHighlightTransform.gameObject.SetActive(false);
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
}
