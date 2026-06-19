using ProjectConnections.Magnetic.Modules;
using UnityEngine;
using Sirenix.OdinInspector;

public class Aiming : MagnetState
{
    MagnetContext _context;

    public void Aim(MagnetContext context) { }

    public void Release(MagnetContext context)
    {
        context.MagnetAiming.StopAiming();
        context.SetState(new Standby());
    }
}
