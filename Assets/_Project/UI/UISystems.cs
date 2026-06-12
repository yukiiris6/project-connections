using UnityEngine;

public class UISystems : MonoBehaviour
{
    [SerializeField] private OverlayCanvas overlayCanvas;

    static UISystems instance;
    public static UISystems Instance => instance;
    public OverlayCanvas OverlayCanvas => overlayCanvas;

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
