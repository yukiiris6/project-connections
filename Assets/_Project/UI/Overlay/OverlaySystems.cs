using UnityEngine;

public class OverlaySystems : MonoBehaviour
{
    [field: SerializeField] public CursorPresenter CursorPresenter { get; private set; }
    [field: SerializeField] public SquareIrisWipe SquareIrisWipe { get; private set; }
    [field: SerializeField] public LevelEnterTransition LevelEnterTransition { get; private set; }
    [field: SerializeField] public BlackScreenTransition BlackScreenTransition { get; private set; }

    static OverlaySystems instance;
    public static OverlaySystems Instance => instance;

    void Awake()
    {
        if (FindObjectsByType<SceneLoaderBrain>(FindObjectsSortMode.None).Length > 1)
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
