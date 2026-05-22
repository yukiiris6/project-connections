using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class LevelManager : MonoBehaviour
{
    [SerializeField] LevelDataSO[] levelSOs;
    [SerializeField] AudioClip deathSFX;
    [SerializeField] float doorAnimationDuration = 1f;

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
        squareIrisWipeController = FindFirstObjectByType<SquareIrisWipeController>();
        cursorController = OverlayCanvas.Instance.CursorController;
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

    public void LoadLevel(string name)
    {
        var found = Array.Find(levels, level => level.fileName == name);
        if (found != null) isInTitleScreen = false;
        else isInTitleScreen = true;
        currentLevel = Array.IndexOf(levels, found);
        StartCoroutine(LoadLevelRoutine(name, 0, false));
    }

    public void FinishLevel()
    {
        levels[currentLevel].SetIsFinished(true);
        if (currentLevel < levels.Count() - 1)
        {
            var nextLevel = levels[currentLevel + 1];
            levels[currentLevel + 1].SetIsLocked(false);
            currentLevel = Array.IndexOf(levels, nextLevel);
            StartCoroutine(LoadLevelRoutine(nextLevel.fileName, doorAnimationDuration, true));
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
        StartCoroutine(RestartRoutine());
    }

    public void GoToTitleScreen()
    {
        isInTitleScreen = true;
        StartCoroutine(LoadLevelRoutine("TitleScreen", 0, false));
    }

    IEnumerator LoadLevelRoutine(string name, float startTime, bool shouldWait)
    {
        if (shouldWait)
        {
            yield return new WaitForSecondsRealtime(1f);
        }
        IsLoading = true;
        yield return new WaitForSecondsRealtime(startTime);
        squareIrisWipeController.StartIrisWipe();
        yield return new WaitForSecondsRealtime(squareIrisWipeController.Duration + .2f);
        GlobalSystems.Instance.GameManager.ResumeGame();
        SceneManager.LoadScene(name);
        yield return null;
        GetDependencies();
        squareIrisWipeController.StartIrisOpen();
        cursorController.ShowCursor();
        IsLoading = false;
        yield return new WaitForSeconds(squareIrisWipeController.Duration);
        if (!isInTitleScreen) GlobalSystems.Instance.MusicManager.PlayMusic();
    }

    IEnumerator DieRoutine()
    {
        audioSource.PlayOneShot(deathSFX);
        GlobalSystems.Instance.MusicManager.StopMusic();
        yield return new WaitForSecondsRealtime(1.5f);
        StartCoroutine(RestartRoutine());
    }

    IEnumerator RestartRoutine()
    {
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
