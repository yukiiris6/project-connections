using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic.Modules
{
    public interface PlugModule
    {
        Energizer Energizer { get; }
        PlugCarryRange PlugCarryRange { get; }
    }
}
