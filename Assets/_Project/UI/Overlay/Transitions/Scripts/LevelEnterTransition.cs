using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI.Extensions;

namespace ProjectConnections.UI.Overlay
{
    public class LevelEnterTransition : MonoBehaviour
    {
        [Header("References")]
        [SerializeField, Required] RectTransform transitionRect;
        [SerializeField, Required] CanvasGroup canvasGroup;
        [SerializeField, Required] TMP_Text levelNumberText;
        [SerializeField, Required] TMP_Text levelNameText;

        [Header("Animation Components")]
        [SerializeField, Required] RectTransform magnetRect;
        [SerializeField, Required] RectTransform plugRect;
        [SerializeField, Required] UILineRenderer leftLine;
        [SerializeField, Required] UILineRenderer rightLine;
        [SerializeField, Required] RectTransform connectionAnchor;
        [SerializeField, Required] AudioSource audioSource;
        [SerializeField, Required] AudioClip plugSFX;
        [SerializeField, Required] float horizontalOffset = 5.5f;

        [Header("Settings")]
        [SerializeField, Required] float fadeDuration = .5f;
        [SerializeField, Required] float pullDuration = .5f;
        [SerializeField, Required] float shakeDuration = .25f;
        [SerializeField, Required] float shakeStrength = 5f;
        [SerializeField, Required] float middleDuration = 1f;

        Vector2 magnetRectOriginalPos;
        Vector2 plugRectOriginalPos;

        void Start()
        {
            magnetRectOriginalPos = magnetRect.anchoredPosition;
            plugRectOriginalPos = plugRect.anchoredPosition;
        }

        public void SetText(int levelNumber, string levelName)
        {
            levelNumberText.text = $"Level {levelNumber}";
            levelNameText.text = levelName;
        }

        public IEnumerator FadeInAndOut()
        {
            HideMagnetComponents();

            FadeIn();
            yield return new WaitForSeconds(fadeDuration);

            yield return new WaitForSeconds(middleDuration);

            FadeOut();
            yield return new WaitForSeconds(fadeDuration);
        }

        public IEnumerator PlayMagnetAnimation()
        {
            SetupMagnetComponents();

            FadeIn();
            yield return new WaitForSeconds(fadeDuration);

            StartMagnetAnimation();
            float magnetAnimationDuration = (shakeDuration * 2) + pullDuration;
            yield return new WaitForSeconds(magnetAnimationDuration);

            FadeOut();
            yield return new WaitForSeconds(fadeDuration);
        }

        void FadeIn()
        {
            canvasGroup.DOFade(1, fadeDuration).SetUpdate(true).SetEase(Ease.InCubic);
        }

        void FadeOut()
        {
            canvasGroup.DOFade(0, fadeDuration).SetUpdate(true).SetEase(Ease.OutCubic);
        }

        void StartMagnetAnimation()
        {
            plugRect.DOShakePosition(shakeDuration, shakeStrength)
                .SetEase(Ease.Linear)
                .SetUpdate(true)
                .OnComplete(() =>
                    plugRect.DOMove(connectionAnchor.position, pullDuration)
                        .SetEase(Ease.InCubic)
                        .SetUpdate(true)
                        .OnUpdate(() => SetupLaser())
                        .OnComplete(() =>
                        {
                            transitionRect.DOShakePosition(shakeDuration, shakeStrength);
                            audioSource.PlayOneShot(plugSFX);
                        })
                );
        }

        void SetupMagnetComponents()
        {
            magnetRect.gameObject.SetActive(true);
            plugRect.gameObject.SetActive(true);
            magnetRect.anchoredPosition = magnetRectOriginalPos;
            plugRect.anchoredPosition = plugRectOriginalPos;
            SetupLaser();
        }

        void HideMagnetComponents()
        {
            magnetRect.gameObject.SetActive(false);
            plugRect.gameObject.SetActive(false);
            leftLine.enabled = false;
            rightLine.enabled = false;
        }

        void SetupLaser()
        {
            Vector2 magnetcenter = magnetRect.anchoredPosition;
            Vector2 plugCenter = plugRect.anchoredPosition;

            Vector2 leftMagnetPole = magnetcenter + new Vector2(horizontalOffset, 0);
            Vector2 rightMagnetPole = magnetcenter + new Vector2(-horizontalOffset, 0);

            Vector2 leftPlugTarget = plugCenter + new Vector2(horizontalOffset, 0);
            Vector2 rightPlugTarget = plugCenter + new Vector2(-horizontalOffset, 0);
            Vector2[] leftPoints = new Vector2[2] { leftMagnetPole, leftPlugTarget };
            leftLine.Points = leftPoints;
            leftLine.SetAllDirty();

            Vector2[] rightPoints = new Vector2[2] { rightMagnetPole, rightPlugTarget };
            rightLine.Points = rightPoints;
            rightLine.SetAllDirty();

            leftLine.enabled = true;
            rightLine.enabled = true;
        }
    }
}
