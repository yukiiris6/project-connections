using UnityEngine;

namespace ProjectConnections.Magnetic
{
    public class ArrivalConstrainer : MonoBehaviour, Constrainer
    {
        [SerializeField] float arrivalDistance = 1.5f;

        public Vector2 GetConstrained(Vector2 destination)
        {
            Vector2 currentPosition = transform.position;
            Vector2 direction = (destination - currentPosition).normalized;
            return destination - (direction * arrivalDistance);
        }
    }
}
