using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Core
{
    public class CoreSystems : MonoBehaviour
    {
        [field: SerializeField, Required] public SceneLoaderBrain SceneLoaderBrain { get; private set; }
        [field: SerializeField, Required] public GameStateSetterBrain GameStateSetter { get; private set; }
        [field: SerializeField, Required] public MusicPlayer MusicPlayer { get; private set; }
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
}
