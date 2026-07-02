using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.SceneManagement;
using ProjectConnections.Level;

namespace ProjectConnections.Core
{
    public class SceneLoaderBrain : MonoBehaviour, SceneContext
    {
        [field: SerializeField, Required] public LevelDataStorage LevelDataStorage { get; private set; }
        [field: SerializeField, Required] public SceneLoader SceneLoader { get; private set; }
        [field: SerializeField, Required] public MusicPlayer MusicPlayer { get; private set; }
        [field: SerializeField, Required] public float LoadDelayTime { get; } = 1f;

        SceneState currentState = new LoadingState();

        void Awake()
        {
            currentState.Enter(this);
        }

        public void RestartLevel()
        {
            currentState.RestartLevel(this);
        }

        public void GoToLevel(string sceneName)
        {
            currentState.GoToLevel(this, sceneName);
        }

        public void GoToTitleScreen()
        {
            currentState.GoToLevel(this, "TitleScreen");
        }

        public void GoToCredits()
        {
            currentState.GoToLevel(this, "CreditsScreen");
        }

        public void FinishLevel()
        {
            currentState.FinishLevel(this);
        }

        public void SetState(SceneState newState)
        {
            currentState.Exit(this);
            currentState = newState;
            currentState.Enter(this);
        }
    }
}
