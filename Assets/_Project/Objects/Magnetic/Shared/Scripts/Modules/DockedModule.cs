using ProjectConnections.Magnetism.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetism.Modules
{
    public interface DockedModule
    {
        Vector2 OriginalPosition { get; }
        void MagnetizeDock();
        void DemagnetizeDock();
    }
}