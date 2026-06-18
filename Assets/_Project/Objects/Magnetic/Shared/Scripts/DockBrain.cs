using ProjectConnections.Magnetism.Modules;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetism
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
