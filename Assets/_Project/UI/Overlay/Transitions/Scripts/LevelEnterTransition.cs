using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class LevelEnterTransition : MonoBehaviour
{
    [Header("References")]
    [SerializeField] RectTransform transitionRect;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TMP_Text levelNumberText;
    [SerializeField] TMP_Text levelNameText;

    [Header("Animation Components")]
    [SerializeField] RectTransform magnetRect;
    [SerializeField] RectTransform plugRect;
    [SerializeField] UILineRenderer leftLine;
    [SerializeField] UILineRenderer rightLine;
    [SerializeField] RectTransform connectionAnchor;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip plugSFX;
    [SerializeField] float horizontalOffset = 5.5f;

    [Header("Settings")]
    [SerializeField] float FadeDuration = .5f;
    [SerializeField] float PullDuration = .5f;
    [SerializeField] float ShakeDuration = .25f;
    [SerializeField] float ShakeStrength = 5f;

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
        FadeIn();
        yield return new WaitForSeconds(FadeDuration);

        FadeOut();
        yield return new WaitForSeconds(FadeDuration);
    }

    public IEnumerator PlayMagnetAnimation()
    {
        SetupMagnetComponents();

        FadeIn();
        yield return new WaitForSeconds(FadeDuration);

        StartMagnetAnimation();
        float magnetAnimationDuration = (ShakeDuration * 2) + PullDuration;
        yield return new WaitForSeconds(magnetAnimationDuration);

        FadeOut();
        yield return new WaitForSeconds(FadeDuration);
    }

    void FadeIn()
    {
        canvasGroup.DOFade(1, FadeDuration).SetUpdate(true).SetEase(Ease.InCubic);
    }

    void FadeOut()
    {
        canvasGroup.DOFade(0, FadeDuration).SetUpdate(true).SetEase(Ease.OutCubic);
    }

    void StartMagnetAnimation()
    {
        plugRect.DOShakePosition(ShakeDuration, ShakeStrength)
            .SetEase(Ease.Linear)
            .SetUpdate(true)
            .OnComplete(() =>
                plugRect.DOMove(connectionAnchor.position, PullDuration)
                    .SetEase(Ease.InCubic)
                    .SetUpdate(true)
                    .OnUpdate(() => SetupLaser())
                    .OnComplete(() =>
                    {
                        transitionRect.DOShakePosition(ShakeDuration, ShakeStrength);
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
