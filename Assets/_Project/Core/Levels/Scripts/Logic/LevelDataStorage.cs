using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDataStorage : MonoBehaviour
{
    [SerializeField] LevelDataSO[] levelSOs;

    LevelData[] availableLevels;
    public LevelData currentLevel { get; private set; }
    public LevelData[] Levels => availableLevels;
    int currentLevelIndex = 0;

    void Awake()
    {
        bool isFirstLevel = true;
        List<LevelData> newLevels = new();
        foreach (var levelSO in levelSOs)
        {
            LevelData newLevel = new LevelData(levelSO);
            if (!newLevel.isMenu && isFirstLevel)
            {
                newLevel.SetIsLocked(false);
                isFirstLevel = false;
            }
            newLevels.Add(newLevel);
        }
        availableLevels = newLevels.ToArray();
        var found = Array.Find(availableLevels, level => level.fileName == SceneManager.GetActiveScene().name);
        if (found != null)
        {
            currentLevelIndex = Array.IndexOf(availableLevels, found);
        }
        else
        {
            currentLevelIndex = -1;
        }
    }

    public string GetCurrentDisplayName()
    {
        if (currentLevelIndex == -1) return SceneManager.GetActiveScene().name;
        return availableLevels[currentLevelIndex].levelDisplayName;
    }

    public string GetCurrentSceneName()
    {
        if (currentLevelIndex == -1) return SceneManager.GetActiveScene().name;
        return availableLevels[currentLevelIndex].fileName;
    }

    public int GetCurrentLevelNumber()
    {
        return currentLevelIndex + 1;
    }

    public bool CurrentLevelIsMenu()
    {
        if (currentLevelIndex == -1) return false;
        return availableLevels[currentLevelIndex].isMenu;
    }

    public AudioClip GetCurrentLevelMusic()
    {
        if (currentLevelIndex == -1) return null;
        return availableLevels[currentLevelIndex].levelMusic;
    }

    public void ChangeCurrentLevel(string fileName)
    {
        var found = Array.Find(availableLevels, level => level.fileName == fileName);
        if (found != null)
        {
            currentLevelIndex = Array.IndexOf(availableLevels, found);
        }
        else
        {
            currentLevelIndex = -1;
        }
    }

    public void ChangeToNextLevel()
    {
        if (currentLevelIndex < availableLevels.Count() - 1)
        {
            var nextLevel = availableLevels[currentLevelIndex + 1];
            ChangeCurrentLevel(nextLevel.fileName);
        }
    }

    public void FinishCurrentLevel()
    {
        availableLevels[currentLevelIndex].SetIsFinished(true);
        if (currentLevelIndex < availableLevels.Count() - 1)
        {
            var nextLevel = availableLevels[currentLevelIndex + 1];
            nextLevel.SetIsLocked(false);
        }
    }
}