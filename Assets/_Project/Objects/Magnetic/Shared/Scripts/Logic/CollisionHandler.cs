using UnityEngine;

namespace ProjectConnections.Magnetism
{
    public class CollisionHandler : MonoBehaviour
    {
        [SerializeField] LayerMask floorLayer;
        [SerializeField] BoxCollider2D mainCollider;

        RaycastHit2D hit;

        public RaycastHit2D GetBoxCastInDirection(Vector2 direction, float moveMagnitude)
        {
            hit = Physics2D.BoxCast(
                mainCollider.bounds.center,
                mainCollider.bounds.size,
                0f,
                direction.normalized,
                moveMagnitude,
                floorLayer
            );
            return hit;
        }
    }
}