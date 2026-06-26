using ProjectConnections.Magnetic.Modules;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic
{
    public class DockBrain : MonoBehaviour, MagnetismModule
    {
        [SerializeField, Required] GameObject magnetizableObject;

        AnchorModule dockedModule;

        void Awake()
        {
            dockedModule = magnetizableObject.GetComponent<AnchorModule>();
        }

        public void Magnetize(Vector2 destination)
        {
            dockedModule.MagnetizeAnchor();
        }

        public void Demagnetize()
        {
            dockedModule.DemagnetizeAnchor();
        }
    }
}
