using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic
{
    public class CollisionHandler : MonoBehaviour
    {
        [SerializeField, Required] LayerMask floorLayer;
        [SerializeField, Required] BoxCollider2D mainCollider;
        [SerializeField, Required] float sizeReduction = .25f;

        RaycastHit2D hit;

        public RaycastHit2D GetBoxCastInDirection(Vector2 direction, float moveMagnitude)
        {
            Vector2 vectorReduction = Vector2.one * sizeReduction;
            hit = Physics2D.BoxCast(
                mainCollider.bounds.center,
                (Vector2)mainCollider.bounds.size - vectorReduction,
                0f,
                direction.normalized,
                moveMagnitude,
                floorLayer
            );
            return hit;
        }
    }
}
