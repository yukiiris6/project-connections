using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using ProjectConnections.UI.Overlay;

namespace ProjectConnections.UIShared
{
    public class BasicButtonPresenter : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField, Required] Button button;
        [SerializeField, Optional] SelectionHighlightAnimation selectionHighlightAnimation;
        [SerializeField, Required] ButtonSoundPlayer buttonSoundPlayer;

        CursorPresenter cursorPresenter;

        void Start()
        {
            GetDependencies();
            button.onClick.AddListener(BasicButtonClick);
        }

        void GetDependencies()
        {
            if (cursorPresenter != null) return;
            cursorPresenter = OverlaySystems.Instance.CursorPresenter;
        }

        public void Init(SelectionHighlightAnimation selectionHighlightAnimation, ButtonSoundPlayer buttonSoundPlayer)
        {
            this.selectionHighlightAnimation = selectionHighlightAnimation;
            this.buttonSoundPlayer = buttonSoundPlayer;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            GetDependencies();
            if (button.interactable == true)
            {
                cursorPresenter.ChangeToInteractionCursor();
                buttonSoundPlayer.PlaySelectSFX();
                if (selectionHighlightAnimation != null)
                {
                    selectionHighlightAnimation.ShowHighlightAt(transform.position);
                }
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            GetDependencies();
            if (button.interactable == true)
            {
                cursorPresenter.ChangeToNormalCursor();
                if (selectionHighlightAnimation != null)
                {
                    selectionHighlightAnimation.HideHighlight();
                }
            }
        }

        void BasicButtonClick()
        {
            cursorPresenter.ChangeToNormalCursor();
            if (selectionHighlightAnimation != null)
            {
                selectionHighlightAnimation.HideHighlight();
            }
        }
    }
}
