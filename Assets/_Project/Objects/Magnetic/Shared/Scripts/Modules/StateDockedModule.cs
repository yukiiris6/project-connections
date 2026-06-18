using ProjectConnections.Magnetism.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetism.Modules
{
    public interface StateDockedModule
    {
        void MagnetizeDock(IContext context);
        void DemagnetizeDock(IContext context);
    }
}