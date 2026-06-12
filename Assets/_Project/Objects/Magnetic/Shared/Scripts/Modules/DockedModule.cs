using ProjectConnections.Magnetism.States;
using UnityEngine;

namespace ProjectConnections.Magnetism.Modules
{
    public interface DockedModule
    {
        Vector2 OriginalPosition { get; }
        void MagnetizeDock();
        void DemagnetizeDock();
    }
}