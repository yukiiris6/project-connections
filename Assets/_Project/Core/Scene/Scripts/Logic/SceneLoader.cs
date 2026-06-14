using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] LevelDataStorage levelDataStorage;
    [SerializeField] SceneTransitionPlayer sceneTransitionPlayer;

    public event Action OnLevelLoad;

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

        if (isFromMenu) yield return sceneTransitionPlayer.PlayFadeOut();
        else yield return sceneTransitionPlayer.PlayIrisWipe();

        yield return LoadSceneSequence(fileName);

        if (isToMenu) yield return sceneTransitionPlayer.PlayFadeIn();
        else yield return sceneTransitionPlayer.PlayIrisOpen();

        if (!isToMenu && !isRestarting)
        {
            int currentLevelNumber = levelDataStorage.GetCurrentLevelNumber();
            string currentLevelName = levelDataStorage.GetCurrentDisplayName();
            yield return sceneTransitionPlayer.PlayLevelEnterSequence(isFromMenu, currentLevelNumber, currentLevelName);
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

        yield return null;
    }
}