using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.Modules
{
    public interface MagnetismModule
    {
        void Magnetize(Vector2 destination);
        void Demagnetize();
    }
}