using System;
using UnityEngine;
using Sirenix.OdinInspector;

public class LevelData
{
    public string FileName { get; private set; }
    public string DisplayName { get; private set; }
    public LevelState State { get; private set; }
    public LevelType Type { get; private set; }
    public AudioClip Music { get; private set; }

    public LevelData(LevelDataSO levelDataSO)
    {
        FileName = levelDataSO.SceneName;
        DisplayName = levelDataSO.DisplayName;
        Type = levelDataSO.Type;
        Music = levelDataSO.Music;
    }

    public void SetState(LevelState value)
    {
        State = value;
    }
}