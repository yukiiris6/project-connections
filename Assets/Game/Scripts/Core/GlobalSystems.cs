using UnityEngine;

public class GlobalSystems : MonoBehaviour
{
    [SerializeField] LevelManager levelManager;
    [SerializeField] GameManager gameManager;
    [SerializeField] MusicManager musicManager;

    static GlobalSystems instance;
    public static GlobalSystems Instance => instance;

    public LevelManager LevelManager => levelManager;
    public GameManager GameManager => gameManager;
    public MusicManager MusicManager => musicManager;

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
