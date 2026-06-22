using ProjectConnections.Magnetic.Modules;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic
{
    public class DockBrain : MonoBehaviour, MagnetismModule
    {
        [SerializeField, Required] GameObject magnetizableObject;

        DockedModule dockedModule;

        void Awake()
        {
            dockedModule = magnetizableObject.GetComponent<DockedModule>();
        }

        public void Magnetize(Vector2 destination)
        {
            dockedModule.MagnetizeDock();
        }

        public void Demagnetize()
        {
            dockedModule.DemagnetizeDock();
        }
    }
}
