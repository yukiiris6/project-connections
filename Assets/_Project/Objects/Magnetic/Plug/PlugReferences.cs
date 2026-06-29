using UnityEngine;

namespace ProjectConnections.Magnetic
{
    public class PlugReferences : MonoBehaviour
    {
        [field: SerializeField] public Mover Mover { get; private set; }
        [field: SerializeField] public Rotator Rotator { get; private set; }
    }
}
