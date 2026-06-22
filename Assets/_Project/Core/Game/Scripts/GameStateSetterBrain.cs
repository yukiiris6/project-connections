using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;

namespace ProjectConnections.Core
{
    public class GameStateSetterBrain : MonoBehaviour, GameContext
    {
        [field: SerializeField] public TimeController TimeController { get; private set; }
        GameState currentState = new NormalState();

        public void SlowdownGame()
        {
            currentState.Slowdown(this);
        }
        public void PauseGame()
        {
            currentState.Pause(this);
        }
        public void ResumeGame()
        {
            currentState.Resume(this);
        }
        public void SetState(GameState newState)
        {
            currentState = newState;
        }
    }
}
