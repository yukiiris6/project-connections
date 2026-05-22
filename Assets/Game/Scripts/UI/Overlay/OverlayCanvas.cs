using UnityEngine;

public class OverlayCanvas : MonoBehaviour
{
    [SerializeField] CursorController cursorController;

    static OverlayCanvas instance;
    public static OverlayCanvas Instance => instance;
    public CursorController CursorController => cursorController;

    void Awake()
    {
        if (FindObjectsByType<LevelManager>(FindObjectsSortMode.None).Length > 1)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
}
