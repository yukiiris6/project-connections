using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoaderBrain : MonoBehaviour, LevelContext
{
    [field: SerializeField] public LevelDataStorage LevelDataStorage { get; private set; }
    [field: SerializeField] public SceneLoader SceneLoader { get; private set; }
    [field: SerializeField] public SceneMusicPlayer SceneMusicPlayer { get; private set; }

    LevelState currentState;

    public void RestartLevel()
    {
        currentState.RestartLevel(this);
    }

    public void GoToLevel(string fileName)
    {
        currentState.GoToLevel(this, fileName);
    }

    public void GoToTitleScreen()
    {
        currentState.GoToLevel(this, "TitleScreen");
    }

    public void GoToCredits()
    {
        currentState.GoToLevel(this, "Credits");
    }

    public void FinishLevel()
    {
        currentState.FinishLevel(this);
    }

    public void SetState(LevelState newState)
    {
        currentState.Exit(this);
        currentState = newState;
        currentState.Enter(this);
    }
}
