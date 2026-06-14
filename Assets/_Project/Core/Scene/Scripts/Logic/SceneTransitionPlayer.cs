using System.Collections;
using DG.Tweening;
using UnityEngine;

public class SceneTransitionPlayer : MonoBehaviour
{
    SquareIrisWipe squareIrisWipe;
    BlackScreenTransition blackScreenTransition;
    LevelEnterTransition levelEnterTransition;

    void GetDependencies()
    {
        if (squareIrisWipe != null && blackScreenTransition != null && levelEnterTransition != null) return;
        squareIrisWipe = OverlaySystems.Instance.SquareIrisWipe;
        blackScreenTransition = OverlaySystems.Instance.BlackScreenTransition;
        levelEnterTransition = OverlaySystems.Instance.LevelEnterTransition;
    }

    void SetupTransition()
    {
        GetDependencies();
        DOTween.KillAll();
    }

    public IEnumerator PlayIrisWipe()
    {
        SetupTransition();
        squareIrisWipe.PlayIrisWipe();
        yield return new WaitForSecondsRealtime(squareIrisWipe.Duration);
    }

    public IEnumerator PlayIrisOpen()
    {
        SetupTransition();
        squareIrisWipe.PlayIrisOpen();
        yield return new WaitForSecondsRealtime(squareIrisWipe.Duration);
    }

    public IEnumerator PlayFadeOut()
    {
        SetupTransition();
        blackScreenTransition.FadeOut();
        yield return new WaitForSecondsRealtime(blackScreenTransition.FadeDuration);
    }

    public IEnumerator PlayFadeIn()
    {
        SetupTransition();
        blackScreenTransition.FadeIn();
        yield return new WaitForSecondsRealtime(blackScreenTransition.FadeDuration);
    }

    public IEnumerator PlayLevelEnterSequence(bool shouldPlayFullAnimation, int levelNumber, string levelName)
    {
        DOTween.KillAll();
        levelEnterTransition.SetText(levelNumber, levelName);

        if (shouldPlayFullAnimation)
        {
            yield return levelEnterTransition.PlayMagnetAnimation();
        }
        else
        {
            yield return levelEnterTransition.FadeInAndOut();
        }

        yield return new WaitForSecondsRealtime(.5f);
    }
}