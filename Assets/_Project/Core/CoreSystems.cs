using UnityEngine;

public class CoreSystems : MonoBehaviour
{
    [field: SerializeField] public SceneLoaderBrain SceneLoader { get; private set; }
    [field: SerializeField] public GameStateSetterBrain GameStateSetter { get; private set; }
    [field: SerializeField] public MusicPlayer MusicPlayer { get; private set; }

    static CoreSystems instance;
    public static CoreSystems Instance => instance;

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
