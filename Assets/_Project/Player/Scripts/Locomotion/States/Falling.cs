using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Player
{
    public class Falling : LocomotionState
    {
        LocomotionContext _context;

        public void Enter(LocomotionContext context)
        {
            _context = context;
            context.GroundValidator.OnLand += OnLand;
        }

        public void Exit(LocomotionContext context)
        {
            context.GroundValidator.OnLand -= OnLand;
        }

        public void Jump(LocomotionContext context) { }

        public void Release(LocomotionContext context) { }

        void OnLand()
        {
            _context.SetState(new Grounded());
            _context.Presenter.InstantiateLandDust();
        }
    }
}
