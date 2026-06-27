using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic
{
    public interface Magnetism
    {
        void Magnetize(Vector2 destination);
        void Demagnetize();
    }
}
