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
    [SerializeField] float doorAnimationDuration = 1f;

    LevelTransition levelTransition;
    AudioSource audioSource;
    LevelData[] levels;
    SquareIrisWipeController squareIrisWipeController;
    CursorController cursorController;
    int currentLevel = 0;
    public LevelData[] Levels => levels;
    public bool IsLoading { get; private set; } = false;
    bool isInTitleScreen = true;

    void GetDependencies()
    {
        cursorController = OverlayCanvas.Instance.CursorController;
        squareIrisWipeController = OverlayCanvas.Instance.SquareIrisWipeController;
        levelTransition = OverlayCanvas.Instance.LevelTransition;
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
            isInTitleScreen = false;
            currentLevel = Array.IndexOf(levels, found);
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
        StartCoroutine(LoadLevelRoutine(name, displayName, 0, true, true));
    }

    public void FinishLevel()
    {
        levels[currentLevel].SetIsFinished(true);
        if (currentLevel < levels.Count() - 1)
        {
            var nextLevel = levels[currentLevel + 1];
            levels[currentLevel + 1].SetIsLocked(false);
            currentLevel = Array.IndexOf(levels, nextLevel);
            StartCoroutine(LoadLevelRoutine(nextLevel.fileName, nextLevel.levelDisplayName, doorAnimationDuration, true, false));
            GlobalSystems.Instance.MusicManager.PlayMusic();
        }
        else
        {
            GoToTitleScreen();
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

    public void GoToTitleScreen()
    {
        isInTitleScreen = true;
        StartCoroutine(LoadLevelRoutine("TitleScreen", "TitleScreen", 0, false, false));
    }

    IEnumerator LoadLevelRoutine(string name, string displayName, float startTime, bool shouldTransition, bool shouldPlayFirstTransition)
    {
        DOTween.KillAll();
        IsLoading = true;
        yield return new WaitForSecondsRealtime(startTime);
        squareIrisWipeController.StartIrisWipe();
        yield return new WaitForSecondsRealtime(squareIrisWipeController.Duration + .2f);
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
        squareIrisWipeController.StartIrisOpen();
        cursorController.ShowCursor();
        yield return new WaitForSeconds(squareIrisWipeController.Duration);
        IsLoading = false;
        if (!isInTitleScreen) GlobalSystems.Instance.MusicManager.PlayMusic();
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
