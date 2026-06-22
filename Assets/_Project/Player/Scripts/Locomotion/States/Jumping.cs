using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Player
{
    public class Jumping : LocomotionState
    {
        LocomotionContext _context;

        public void Enter(LocomotionContext context)
        {
            _context = context;
            context.Jumper.OnFall += OnFall;
            context.GroundValidator.OnLand += OnLand;
        }

        public void Exit(LocomotionContext context)
        {
            context.Jumper.OnFall -= OnFall;
            context.GroundValidator.OnLand -= OnLand;
        }

        public void Jump(LocomotionContext context) { }

        public void Release(LocomotionContext context)
        {
            context.Jumper.StopJump();
            context.SetState(new Falling());
        }

        void OnFall()
        {
            _context.SetState(new Falling());
        }

        void OnLand()
        {
            _context.SetState(new Grounded());
            _context.Presenter.InstantiateLandDust();
        }
    }
}
