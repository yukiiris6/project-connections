using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.Modules
{
    public interface StateDockedModule
    {
        void MagnetizeDock(IContext context);
        void DemagnetizeDock(IContext context);
    }
}
