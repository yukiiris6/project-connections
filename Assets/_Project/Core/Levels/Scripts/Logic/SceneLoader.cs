using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] LevelDataStorage levelDataStorage;

    CursorController cursorController;
    LevelTransition levelTransition;
    CanvasGroup blackScreen;
    SquareIrisWipeController irisWipeController;

    public event Action OnLevelLoad;

    void GetDependencies()
    {
        cursorController = UISystems.Instance.OverlayCanvas.CursorController;
        irisWipeController = UISystems.Instance.OverlayCanvas.SquareIrisWipeController;
        levelTransition = UISystems.Instance.OverlayCanvas.LevelTransition;
        blackScreen = UISystems.Instance.OverlayCanvas.BlackScreen;
    }

    // IEnumerator LoadLevelRoutine(int levelNumber, string fileName, string displayName)
    // {
    //     yield return new WaitForSecondsRealtime(startTime);
    //     DOTween.KillAll();
    //     if (isFromMenus) yield return PreLoadFade();
    //     else yield return PreLoadIrisWipe();

    //     yield return LoadSceneSequence(fileName);
    //     PlayLevelTransition(true, levelNumber, displayName);
    //     if (isToMenus)
    //     {
    //         blackScreen.alpha = 1f;
    //         irisWipeController.ResetIris();
    //         blackScreen.DOFade(0f, 1f).SetEase(Ease.InSine);
    //         if (fileName == "Credits")
    //         {
    //             var found = Array.Find(levels, level => !level.isFinished);
    //             if (found == null) audioSource.PlayOneShot(creditsJingle);
    //         }
    //         CoreSystems.Instance.MusicManager.SetTitleScreenMusic();
    //         yield return new WaitForSeconds(1f);
    //     }
    //     else
    //     {
    //         blackScreen.alpha = 0f;
    //         irisWipeController.StartIrisOpen();
    //         yield return new WaitForSeconds(irisWipeController.Duration);
    //         CoreSystems.Instance.MusicManager.SetLevelMusic();
    //     }
    //     cursorController.ShowCursor();
    //     if (fileName != "Credits")
    //     {
    //         CoreSystems.Instance.MusicManager.PlayMusic();
    //     }
    // }

    public void LoadCurrentScene(bool isFromMenu, bool isToMenu)
    {
        StartCoroutine(SceneTransitionSequence(isFromMenu, isToMenu));
    }

    IEnumerator SceneTransitionSequence(bool isFromMenu, bool isToMenu)
    {
        string fileName = levelDataStorage.GetCurrentFileName();

        if (isFromMenu) yield return PlayFadeOut();
        else yield return PlayIrisWipe();

        yield return LoadSceneSequence(fileName);

        if (isToMenu) yield return PlayFadeIn();
        else yield return PlayIrisOpen();

        if (!isToMenu)
        {
            int currentLevelNumber = levelDataStorage.GetCurrentLevelNumber();
            string currentLevelName = levelDataStorage.GetCurrentDisplayName();
            yield return LevelTransitionSequence(isFromMenu, currentLevelNumber, currentLevelName);
        }

        OnLevelLoad?.Invoke();
    }

    IEnumerator LoadSceneSequence(string fileName)
    {
        AsyncOperation loadLevelOperation = SceneManager.LoadSceneAsync(fileName);

        loadLevelOperation.allowSceneActivation = false;
        while (loadLevelOperation.progress < 0.9f)
        {
            yield return null;
        }

        loadLevelOperation.allowSceneActivation = true;
        while (!loadLevelOperation.isDone)
        {
            yield return null;
        }
    }

    IEnumerator LevelTransitionSequence(bool shouldPlayFullAnimation, int levelNumber, string levelName)
    {
        if (!shouldPlayFullAnimation) levelTransition.HideAnimationComponents();
        else levelTransition.PrepareAnimationComponents();

        levelTransition.SetText(levelNumber, levelName);
        levelTransition.FadeIn();
        yield return new WaitForSecondsRealtime(.5f);

        if (shouldPlayFullAnimation)
        {
            levelTransition.PlayAnimation();
            yield return new WaitForSecondsRealtime(2f);
        }
        else
        {
            yield return new WaitForSecondsRealtime(1f);
        }

        levelTransition.FadeOut();
        yield return new WaitForSecondsRealtime(.5f);
    }

    IEnumerator PlayIrisWipe()
    {
        DOTween.KillAll();
        irisWipeController.StartIrisWipe();
        yield return new WaitForSecondsRealtime(irisWipeController.Duration);
    }

    IEnumerator PlayIrisOpen()
    {
        GetDependencies();
        irisWipeController.StartIrisOpen();
        yield return new WaitForSecondsRealtime(irisWipeController.Duration);
    }

    IEnumerator PlayFadeOut()
    {
        DOTween.KillAll();
        blackScreen.DOFade(1f, 1f).SetEase(Ease.InSine);
        yield return new WaitForSecondsRealtime(1f);
    }

    IEnumerator PlayFadeIn()
    {
        GetDependencies();
        irisWipeController.StartIrisOpen();
        yield return new WaitForSecondsRealtime(irisWipeController.Duration);
    }
}