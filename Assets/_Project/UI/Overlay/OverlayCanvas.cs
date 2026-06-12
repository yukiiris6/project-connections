using UnityEngine;

public class OverlayCanvas : MonoBehaviour
{
    [SerializeField] CursorController cursorController;
    [SerializeField] SquareIrisWipeController squareIrisWipeController;
    [SerializeField] LevelTransition levelTransition;
    [SerializeField] CanvasGroup blackScreen;
    [SerializeField] PauseMenuFlow pauseMenuFlow;

    public CursorController CursorController => cursorController;
    public SquareIrisWipeController SquareIrisWipeController => squareIrisWipeController;
    public LevelTransition LevelTransition => levelTransition;
    public CanvasGroup BlackScreen => blackScreen;
    public PauseMenuFlow PauseMenuFlow => pauseMenuFlow;
}
