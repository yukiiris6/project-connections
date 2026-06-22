using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.Shared;

namespace ProjectConnections.Magnetic.Modules
{
    public interface CarriableModule
    {
        CarriableObject CarriableObject { get; }
    }
}
