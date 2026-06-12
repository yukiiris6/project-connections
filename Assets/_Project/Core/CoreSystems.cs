using UnityEngine;

public class CoreSystems : MonoBehaviour
{
    [SerializeField] SceneLoaderBrain sceneLoader;
    [SerializeField] GameBrain gameBrain;
    [SerializeField] MusicPlayer musicPlayer;

    static CoreSystems instance;
    public static CoreSystems Instance => instance;

    public SceneLoaderBrain SceneLoader => sceneLoader;
    public GameBrain GameBrain => gameBrain;
    public MusicPlayer MusicPlayer => musicPlayer;

    void Awake()
    {
        if (FindObjectsByType<CoreSystems>(FindObjectsSortMode.None).Length > 1)
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
