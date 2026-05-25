using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class LevelManager : MonoBehaviour
{
    [SerializeField] LevelDataSO[] levelSOs;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] AudioClip creditsJingle;
    [SerializeField] float doorAnimationDuration = 1f;

    LevelTransition levelTransition;
    AudioSource audioSource;
    LevelData[] levels;
    SquareIrisWipeController squareIrisWipeController;
    CursorController cursorController;
    CanvasGroup blackScreen;
    int currentLevel = 0;
    public LevelData[] Levels => levels;
    bool isInTitleScreen = true;
    bool isInSplashScreen = false;
    public bool IsLoading { get; private set; } = false;
    public bool IsInTitleScreen => isInTitleScreen;
    public bool IsInSplashScreen => isInSplashScreen;

    void GetDependencies()
    {
        cursorController = OverlayCanvas.Instance.CursorController;
        squareIrisWipeController = OverlayCanvas.Instance.SquareIrisWipeController;
        levelTransition = OverlayCanvas.Instance.LevelTransition;
        blackScreen = OverlayCanvas.Instance.BlackScreen;
    }

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        List<LevelData> newLevels = new();
        foreach (var levelSO in levelSOs)
        {
            LevelData newLevel = new LevelData(levelSO);
            newLevels.Add(newLevel);
        }
        levels = newLevels.ToArray();
        var found = Array.Find(levels, level => level.fileName == SceneManager.GetActiveScene().name);
        if (found != null)
        {
            currentLevel = Array.IndexOf(levels, found);
        }
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "TitleScreen")
        {
            isInTitleScreen = true;
        }
        else if (sceneName == "SplashScreen")
        {
            isInSplashScreen = true;
        }
    }

    void Start()
    {
        if (!isInTitleScreen) GlobalSystems.Instance.MusicManager.PlayMusic();
        GetDependencies();
    }

    public void LoadLevel(string name, string displayName)
    {
        var found = Array.Find(levels, level => level.fileName == name);
        if (found != null) isInTitleScreen = false;
        else isInTitleScreen = true;
        currentLevel = Array.IndexOf(levels, found);
        StartCoroutine(LoadLevelRoutine(name, displayName, 0, true, true, true));
    }

    public void FinishLevel()
    {
        levels[currentLevel].SetIsFinished(true);
        if (currentLevel < levels.Count() - 1)
        {
            var nextLevel = levels[currentLevel + 1];
            levels[currentLevel + 1].SetIsLocked(false);
            currentLevel = Array.IndexOf(levels, nextLevel);
            StartCoroutine(LoadLevelRoutine(nextLevel.fileName, nextLevel.levelDisplayName, doorAnimationDuration, true, false, false));
            GlobalSystems.Instance.MusicManager.PlayMusic();
        }
        else
        {
            GoToCredits(false);
            GlobalSystems.Instance.MusicManager.StopMusic();
        }
    }

    public void Die()
    {
        StartCoroutine(DieRoutine());
    }

    public void RestartLevel()
    {
        StartCoroutine(RestartRoutine(false));
    }

    public void GoToTitleScreen(bool isFromLevel)
    {
        isInTitleScreen = true;
        StartCoroutine(LoadLevelRoutine("TitleScreen", "TitleScreen", 0, false, false, !isFromLevel));
    }

    public void GoToCredits(bool isFromTitleScreen)
    {
        isInTitleScreen = false;
        float startTime = isFromTitleScreen ? 0 : 1;
        StartCoroutine(LoadLevelRoutine("Credits", "Credits", startTime, false, false, isFromTitleScreen));
    }

    IEnumerator LoadLevelRoutine(string name, string displayName, float startTime, bool shouldTransition, bool shouldPlayFirstTransition, bool isFromMenus)
    {
        if (name == "TitleScreen" || name == "Credits" || isFromMenus) GlobalSystems.Instance.MusicManager.StopMusic();
        yield return new WaitForSecondsRealtime(startTime);
        IsLoading = true;
        DOTween.KillAll();
        if (isFromMenus)
        {
            blackScreen.DOFade(1f, 1f).SetEase(Ease.InSine);
            yield return new WaitForSecondsRealtime(1f);
        }
        else
        {
            squareIrisWipeController.StartIrisWipe();
            yield return new WaitForSecondsRealtime(squareIrisWipeController.Duration + .2f);
        }
        GlobalSystems.Instance.GameManager.ResumeGame();
        SceneManager.LoadScene(name);
        yield return null;
        GetDependencies();
        if (shouldTransition)
        {
            levelTransition.SetText(currentLevel + 1, displayName);
            if (!shouldPlayFirstTransition) levelTransition.HideAnimationComponents();
            else levelTransition.PrepareAnimationComponents();
            levelTransition.FadeIn();
            yield return new WaitForSecondsRealtime(.5f);
            if (shouldPlayFirstTransition)
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
        if (name == "TitleScreen" || name == "Credits")
        {
            blackScreen.alpha = 1f;
            squareIrisWipeController.ResetIris();
            blackScreen.DOFade(0f, 1f).SetEase(Ease.InSine);
            if (name == "Credits")
            {
                var found = Array.Find(levels, level => !level.isFinished);
                if (found == null) audioSource.PlayOneShot(creditsJingle);
            }
            GlobalSystems.Instance.MusicManager.SetTitleScreenMusic();
            yield return new WaitForSeconds(1f);
        }
        else
        {
            blackScreen.alpha = 0f;
            squareIrisWipeController.StartIrisOpen();
            yield return new WaitForSeconds(squareIrisWipeController.Duration);
            GlobalSystems.Instance.MusicManager.SetLevelMusic();
        }
        cursorController.ShowCursor();
        IsLoading = false;
        if (name != "Credits")
        {
            GlobalSystems.Instance.MusicManager.PlayMusic();
        }
    }

    IEnumerator DieRoutine()
    {
        audioSource.PlayOneShot(deathSFX);
        GlobalSystems.Instance.MusicManager.StopMusic();
        yield return new WaitForSecondsRealtime(1.5f);
        StartCoroutine(RestartRoutine(true));
    }

    IEnumerator RestartRoutine(bool shouldWait)
    {
        DOTween.KillAll();
        if (shouldWait) yield return new WaitForSecondsRealtime(1f);
        squareIrisWipeController.StartIrisWipe();
        yield return new WaitForSecondsRealtime(squareIrisWipeController.Duration);
        IsLoading = true;
        AsyncOperation loadLevel = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        loadLevel.allowSceneActivation = false;
        while (loadLevel.progress < 0.9f) yield return null;
        loadLevel.allowSceneActivation = true;
        yield return null;
        GetDependencies();
        squareIrisWipeController.StartIrisOpen();
        GlobalSystems.Instance.GameManager.ResumeGame();
        cursorController.ShowCursor();
        IsLoading = false;
        GlobalSystems.Instance.MusicManager.PlayMusic();
    }
}
