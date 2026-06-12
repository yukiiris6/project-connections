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
    string menuFileName;

    void Awake()
    {
        List<LevelData> newLevels = new();
        foreach (var levelSO in levelSOs)
        {
            LevelData newLevel = new LevelData(levelSO);
            newLevels.Add(newLevel);
        }
        availableLevels = newLevels.ToArray();
        var found = Array.Find(availableLevels, level => level.fileName == SceneManager.GetActiveScene().name);
        if (found != null)
        {
            currentLevelIndex = Array.IndexOf(availableLevels, found);
        }
    }


    public string GetCurrentDisplayName()
    {
        if (currentLevelIndex == -1) return menuFileName;
        return availableLevels[currentLevelIndex].levelDisplayName;
    }

    public string GetCurrentFileName()
    {
        if (currentLevelIndex == -1) return menuFileName;
        return availableLevels[currentLevelIndex].fileName;
    }

    public int GetCurrentLevelNumber()
    {
        return currentLevelIndex + 1;
    }

    public bool CurrentLevelIsMenu()
    {
        return currentLevelIndex == -1;
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
            menuFileName = fileName;
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