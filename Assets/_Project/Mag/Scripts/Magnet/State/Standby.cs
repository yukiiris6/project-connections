using UnityEngine;

public class Standby : MagnetState
{
    public void Aim(MagnetContext context)
    {
        context.MagnetAiming.Aim();
        GameObject targetObject = context.MagnetAiming.GetTargetObject();
        context.SetState(new Aiming());
    }

    public void Release(MagnetContext context) { }
}
