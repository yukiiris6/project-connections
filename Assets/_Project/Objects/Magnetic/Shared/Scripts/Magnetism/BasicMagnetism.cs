using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic
{
    public class BasicMagnetism : SerializedMonoBehaviour, Magnetism
    {
        [SerializeField, Required] Mover mover;
        [SerializeField, Required] Rotator rotator;
        [SerializeField, Required] Presenter presenter;
        [SerializeField, Required] Rigidbody2D myRigidbody;
        [SerializeField, Required] Constrainer[] constraints;
        [SerializeField, Required] bool shouldRotate;

        RigidbodyType2D originalBodyType;
        bool isPulling;

        void Awake()
        {
            originalBodyType = myRigidbody.bodyType;
        }

        void OnEnable()
        {
            mover.OnDestinationReached += HandleArrival;
        }

        void OnDisable()
        {
            mover.OnDestinationReached -= HandleArrival;
        }

        void HandleArrival()
        {
            myRigidbody.bodyType = originalBodyType;
            PlayPresenterCrash();
        }

        public void Magnetize(Vector2 destination)
        {
            Vector2 finalDestination = destination;
            myRigidbody.bodyType = RigidbodyType2D.Kinematic;
            myRigidbody.linearVelocity = Vector2.zero;

            foreach (var constraint in constraints)
            {
                finalDestination = constraint.GetConstrained(finalDestination);
            }

            mover.MoveTo(finalDestination);
            UpdatePullingVisuals(destination);
        }

        public void Demagnetize()
        {
            isPulling = false;
            myRigidbody.bodyType = originalBodyType;
            mover.Stop();
            PlayPresenterCrash();
        }

        void PlayPresenterCrash()
        {
            float distanceTravelled = mover.GetDistanceTravelled();
            presenter.PlayStopByDistance(distanceTravelled);
        }

        void UpdatePullingVisuals(Vector2 destination)
        {
            if (!shouldRotate) return;
            if (isPulling) return;
            isPulling = true;
            rotator.RotateTo(destination);
        }
    }
}
