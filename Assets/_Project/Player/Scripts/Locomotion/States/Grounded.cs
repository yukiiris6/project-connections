using UnityEngine;
using Sirenix.OdinInspector;

public class Grounded : LocomotionState
{
    LocomotionContext _context;

    public void Enter(LocomotionContext context)
    {
        _context = context;
        context.GroundValidator.OnExitGround += OnExitGround;
    }

    public void Exit(LocomotionContext context)
    {
        context.GroundValidator.OnExitGround -= OnExitGround;
    }

    public void Jump(LocomotionContext context)
    {
        context.Jumper.Jump();
        context.Presenter.InstantiateJumpDust();
        context.SoundPlayer.PlayJumpSFX();
        context.SetState(new Jumping());
    }

    public void Release(LocomotionContext context) { }

    void OnExitGround()
    {
        _context.SetState(new Falling());
    }
}