using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class OverlayCanvas : MonoBehaviour
{
    [SerializeField] CursorController cursorController;
    [SerializeField] SquareIrisWipeController squareIrisWipeController;
    [SerializeField] LevelTransition levelTransition;
    [SerializeField] CanvasGroup blackScreen;

    static OverlayCanvas instance;
    public static OverlayCanvas Instance => instance;
    public CursorController CursorController => cursorController;
    public SquareIrisWipeController SquareIrisWipeController => squareIrisWipeController;
    public LevelTransition LevelTransition => levelTransition;
    public CanvasGroup BlackScreen => blackScreen;

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
