using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic
{
    public interface Constrainer
    {
        Vector2 GetConstrained(Vector2 destination);
    }
}
