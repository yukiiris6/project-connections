using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.Modules
{
    public interface DockedModule
    {
        Vector2 OriginalPosition { get; }
        void MagnetizeDock();
        void DemagnetizeDock();
    }
}