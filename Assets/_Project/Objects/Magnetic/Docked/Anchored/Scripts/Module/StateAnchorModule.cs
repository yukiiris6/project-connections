using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.Modules
{
    public interface StateAnchorModule
    {
        void MagnetizeAnchor(IContext context);
        void DemagnetizeAnchor(IContext context);
    }
}
