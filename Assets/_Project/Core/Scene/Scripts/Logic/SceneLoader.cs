using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField, Required] LevelDataStorage levelDataStorage;
    [SerializeField, Required] SceneTransitionPlayer sceneTransitionPlayer;

    public event Action OnLevelLoad;

    CursorPresenter cursorPresenter;

    void Start()
    {
        cursorPresenter = OverlaySystems.Instance.CursorPresenter;
    }

    public void LoadCurrentScene(LevelType currentLevelType, LevelType nextLevelType)
    {
        bool isFromMenu = currentLevelType == LevelType.Menu;
        bool isToMenu = nextLevelType == LevelType.Menu;
        StartCoroutine(SceneTransitionSequence(isFromMenu, isToMenu));
    }

    IEnumerator SceneTransitionSequence(bool isFromMenu, bool isToMenu)
    {
        string fileName = levelDataStorage.GetCurrentSceneName();
        string currentSceneName = SceneManager.GetActiveScene().name;
        bool isRestarting = currentSceneName == fileName;

        cursorPresenter.UnallowInteractions();
        if (isFromMenu) yield return sceneTransitionPlayer.PlayFadeOut();
        else yield return sceneTransitionPlayer.PlayIrisWipe();

        DOTween.KillAll();
        yield return LoadSceneSequence(fileName);

        if (isToMenu) yield return sceneTransitionPlayer.PlayFadeIn();
        else yield return sceneTransitionPlayer.PlayIrisOpen();

        if (!isToMenu && !isRestarting)
        {
            int currentLevelNumber = levelDataStorage.GetCurrentLevelNumber();
            string currentLevelName = levelDataStorage.GetCurrentDisplayName();
            yield return sceneTransitionPlayer.PlayLevelEnterSequence(isFromMenu, currentLevelNumber, currentLevelName);
        }

        cursorPresenter.AllowInteractions();

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

        yield return null;
    }
}