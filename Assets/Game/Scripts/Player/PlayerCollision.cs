using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] BoxCollider2D feetCollider;
    [SerializeField] LayerMask movingPlatformsLayer;

    Transform originalParent;

    void Awake()
    {
        originalParent = transform.parent;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.bounds.Intersects(feetCollider.bounds))
        {
            bool isMovingPlatform = ((1 << other.gameObject.layer) & movingPlatformsLayer.value) != 0;
            if (isMovingPlatform) transform.SetParent(other.transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (transform.parent != null) transform.SetParent(originalParent);
    }
}
