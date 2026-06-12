using ProjectConnections.Magnetism.Modules;
using UnityEngine;

namespace ProjectConnections.Magnetism
{
    public class DockBrain : MonoBehaviour, MagnetismModule
    {
        [SerializeField] GameObject magnetizableObject;

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
