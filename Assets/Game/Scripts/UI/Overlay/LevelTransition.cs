using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI.Extensions;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CanvasGroup))]
public class LevelTransition : MonoBehaviour
{
    [Header("Animation Components")]
    [SerializeField] RectTransform container;
    [SerializeField] RectTransform magnetRect;
    [SerializeField] RectTransform plugRect;
    [SerializeField] UILineRenderer leftLine;
    [SerializeField] UILineRenderer rightLine;
    [SerializeField] RectTransform connectionAnchor;
    [SerializeField] AudioClip plugSFX;
    [SerializeField] float horizontalOffset = 5.5f;

    [Header("Text Components")]
    [SerializeField] TMP_Text levelNumberText;
    [SerializeField] TMP_Text levelNameText;

    Vector2 magnetRectOriginalPos;
    Vector2 plugRectOriginalPos;
    AudioSource audioSource;
    CanvasGroup canvasGroup;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        canvasGroup = GetComponent<CanvasGroup>();
        magnetRectOriginalPos = magnetRect.anchoredPosition;
        plugRectOriginalPos = plugRect.anchoredPosition;
    }

    public void PlayAnimation()
    {
        plugRect.DOShakePosition(.3f, 5f)
            .SetEase(Ease.Linear)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                plugRect.DOMove(connectionAnchor.position, .5f)
                    .SetEase(Ease.InCubic)
                    .SetUpdate(true)
                    .OnUpdate(() =>
                    {
                        ShowLaser();
                    })
                    .OnComplete(() =>
                    {
                        container.DOShakePosition(.3f, 5f);
                        audioSource.PlayOneShot(plugSFX);
                    });
            });
    }

    public void PrepareAnimationComponents()
    {
        magnetRect.gameObject.SetActive(true);
        plugRect.gameObject.SetActive(true);
        magnetRect.anchoredPosition = magnetRectOriginalPos;
        plugRect.anchoredPosition = plugRectOriginalPos;
        ShowLaser();
    }

    public void HideAnimationComponents()
    {
        magnetRect.gameObject.SetActive(false);
        plugRect.gameObject.SetActive(false);
        leftLine.enabled = false;
        rightLine.enabled = false;
    }

    public void SetText(int levelNumber, string levelName)
    {
        levelNumberText.text = $"Level {levelNumber}";
        levelNameText.text = levelName;
    }

    public void FadeIn()
    {
        canvasGroup.DOFade(1, .5f).SetUpdate(true).SetEase(Ease.InCubic);
    }

    public void FadeOut()
    {
        canvasGroup.DOFade(0, .5f).SetUpdate(true).SetEase(Ease.OutCubic);
    }

    void ShowLaser()
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
