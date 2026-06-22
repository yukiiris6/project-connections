using UnityEngine;

namespace ProjectConnections.Shared
{
    public class SystemsInitializer : MonoBehaviour
    {
        static SystemsInitializer instance;
        public static SystemsInitializer Instance => instance;

        void Awake()
        {
            if (FindObjectsByType<SystemsInitializer>(FindObjectsSortMode.None).Length > 1)
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

        void Start()
        {
            InitializeSystems();
        }

        void InitializeSystems()
        {
        }
    }
}
