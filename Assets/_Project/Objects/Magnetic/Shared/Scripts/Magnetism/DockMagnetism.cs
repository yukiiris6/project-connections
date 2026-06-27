using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic
{
    public class DockMagnetism : MonoBehaviour, Magnetism
    {
        [SerializeField, Required] Mover mover;
        [SerializeField, Required] Rotator rotator;
        [SerializeField, Required] Presenter presenter;
        [SerializeField, Required] Transform plugTransform;

        Vector2 originalPosition;

        void Awake()
        {
            originalPosition = plugTransform.position;
        }

        void HandleArrival()
        {
            mover.SnapTo(originalPosition);
            mover.OnDestinationReached -= HandleArrival;
        }

        public void Magnetize(Vector2 destination)
        {
            mover.MoveTo(originalPosition);
            rotator.ResetRotation();
            mover.OnDestinationReached += HandleArrival;
        }

        public void Demagnetize()
        {
            mover.Stop();
            mover.OnDestinationReached -= HandleArrival;
        }
    }
}
