using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.Modules
{
    public interface AnchorModule
    {
        AnchorRange AnchorRange { get; }
        void MagnetizeAnchor();
        void DemagnetizeAnchor();
    }
}
