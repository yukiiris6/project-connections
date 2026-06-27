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

        RigidbodyType2D originalBodyType;

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

            foreach (var constraint in constraints)
            {
                finalDestination = constraint.GetConstrained(finalDestination);
            }

            mover.MoveTo(finalDestination);
        }

        public void Demagnetize()
        {
            myRigidbody.bodyType = originalBodyType;
            mover.Stop();
            PlayPresenterCrash();
        }

        void PlayPresenterCrash()
        {
            float distanceTravelled = mover.GetDistanceTravelled();
            presenter.PlayStopByDistance(distanceTravelled);
        }
    }
}
