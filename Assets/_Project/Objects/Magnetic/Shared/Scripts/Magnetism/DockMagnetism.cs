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
        bool isSubscribed;

        void Awake()
        {
            originalPosition = plugTransform.position;
        }

        void Subscribe()
        {
            if (isSubscribed) return;
            mover.OnDestinationReached += HandleArrival;
            isSubscribed = true;
        }

        void Unsubscribe()
        {
            if (!isSubscribed) return;
            mover.OnDestinationReached -= HandleArrival;
            isSubscribed = false;
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
            Subscribe();
        }

        public void Demagnetize()
        {
            mover.Stop();
            Unsubscribe();
        }
    }
}
