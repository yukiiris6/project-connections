using UnityEngine;

namespace ProjectConnections.Magnetic
{
    public class Constrainer : MonoBehaviour
    {
        [SerializeField] float stopDistance = 1.5f;

        public Vector2 ConstrainStopDistance(Vector2 destination)
        {
            Vector2 currentPosition = transform.position;
            Vector2 direction = (destination - currentPosition).normalized;
            return destination - (direction * stopDistance);
        }
    }
}
