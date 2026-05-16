using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    [SerializeField] LayerMask movingPlatformsLayer;

    void OnCollisionEnter2D(Collision2D collision)
    {
        bool isMovingPlatform = ((1 << collision.gameObject.layer) & movingPlatformsLayer.value) != 0;
        if (isMovingPlatform)
        {
            transform.SetParent(collision.transform);
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (transform.parent != null)
        {
            transform.parent = null;
        }
    }
}
