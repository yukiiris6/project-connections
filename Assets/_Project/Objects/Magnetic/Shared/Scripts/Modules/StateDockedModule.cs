using ProjectConnections.Magnetism.States;
using UnityEngine;

namespace ProjectConnections.Magnetism.Modules
{
    public interface StateDockedModule
    {
        void MagnetizeDock(IContext context);
        void DemagnetizeDock(IContext context);
    }
}