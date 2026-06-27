using ProjectConnections.Magnetic;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Player
{
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
}
