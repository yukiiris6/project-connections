using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using ProjectConnections.Level;

namespace ProjectConnections.Core
{
    public class LevelDataStorage : MonoBehaviour
    {
        [SerializeField, Required] LevelDataSO[] levelSOs;

        LevelData[] availableLevels;
        public LevelData currentLevel { get; private set; }
        int currentLevelIndex = 0;

        void Awake()
        {
            SetupLevels();
            var found = Array.Find(availableLevels, level => level.FileName == SceneManager.GetActiveScene().name);
            ChangeCurrentLevel(found?.FileName);
        }

        public LevelData[] GetLevels()
        {
            return availableLevels;
        }

        public string GetCurrentDisplayName()
        {
            if (currentLevelIndex == -1) return SceneManager.GetActiveScene().name;
            return availableLevels[currentLevelIndex].DisplayName;
        }

        public string GetCurrentSceneName()
        {
            if (currentLevelIndex == -1) return SceneManager.GetActiveScene().name;
            return availableLevels[currentLevelIndex].FileName;
        }

        public int GetCurrentLevelNumber()
        {
            return currentLevelIndex + 1;
        }

        public LevelType GetCurrentLevelType()
        {
            if (currentLevelIndex == -1) return LevelType.Stage;
            return availableLevels[currentLevelIndex].Type;
        }

        public AudioClip GetCurrentLevelMusic()
        {
            if (currentLevelIndex == -1) return null;
            return availableLevels[currentLevelIndex].Music;
        }

        public void ChangeCurrentLevel(string fileName)
        {
            var found = Array.Find(availableLevels, level => level.FileName == fileName);
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
            if (currentLevelIndex == -1 || currentLevelIndex > availableLevels.Count() - 1) return;
            var nextLevel = availableLevels[currentLevelIndex + 1];
            ChangeCurrentLevel(nextLevel.FileName);
        }

        public void FinishCurrentLevel()
        {
            if (currentLevelIndex == -1) return;

            availableLevels[currentLevelIndex].SetState(LevelState.Finished);
            if (currentLevelIndex < availableLevels.Count() - 1)
            {
                var nextLevel = availableLevels[currentLevelIndex + 1];
                nextLevel.SetState(LevelState.Unlocked);
            }
        }

        void SetupLevels()
        {
            bool isFirstLevel = true;
            List<LevelData> newLevels = new();
            foreach (var levelSO in levelSOs)
            {
                LevelData newLevel = new LevelData(levelSO);
                if (newLevel.Type == LevelType.Stage && isFirstLevel)
                {
                    newLevel.SetState(LevelState.Unlocked);
                    isFirstLevel = false;
                }
                newLevels.Add(newLevel);
            }
            availableLevels = newLevels.ToArray();
        }
    }
}
