using ProjectConnections.Magnetic.States;
using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.ObjectShared;

namespace ProjectConnections.Magnetic.Modules
{
    public interface CarriableModule
    {
        CarriableObject CarriableObject { get; }
    }
}
