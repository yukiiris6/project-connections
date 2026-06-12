using ProjectConnections.Magnetism.States;
using UnityEngine;

namespace ProjectConnections.Magnetism.Modules
{
    public interface MagnetismModule
    {
        void Magnetize(Vector2 destination);
        void Demagnetize();
    }
}