using UnityEngine;
using UnityEngine.InputSystem;

public class TimeController : MonoBehaviour
{
    [SerializeField][Range(0, 1f)] float slowdownAmount;

    public void PauseTime()
    {
        Time.timeScale = 0;
    }

    public void ResetTime()
    {
        Time.timeScale = 1;
    }

    public void SlowdownTime()
    {
        Time.timeScale = slowdownAmount;
    }
}