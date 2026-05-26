using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CinemachineImpulseSource))]
public class PlayerCollision : MonoBehaviour
{
    [SerializeField] BoxCollider2D feetCollider;
    [SerializeField] LayerMask movingPlatformsLayer;
    [SerializeField] LayerMask obstacleLayer;

    Transform originalParent;

    void Awake()
    {
        originalParent = transform.parent;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        bool isMovingPlatform = ((1 << other.gameObject.layer) & movingPlatformsLayer.value) != 0; ;
        if (isMovingPlatform)
        {
            if (other.IsTouching(feetCollider))
            {
                transform.SetParent(other.transform);
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        bool isMovingPlatform = LayerMaskExtensions.Contains(movingPlatformsLayer, gameObject.layer);
        if (isMovingPlatform)
        {
            if (transform.parent != other.transform && other.IsTouching(feetCollider))
            {
                transform.SetParent(other.transform);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (transform.parent == other.transform)
        {
            if (!other.gameObject.activeInHierarchy || !other.enabled) return;
            transform.SetParent(originalParent);
        }
    }
}
