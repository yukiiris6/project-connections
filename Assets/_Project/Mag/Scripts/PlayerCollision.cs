using System.Linq;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CinemachineImpulseSource))]
public class PlayerCollision : MonoBehaviour
{
    [SerializeField] LayerMask movingPlatformsLayer;
    [SerializeField] LayerMask obstacleLayer;

    BoxCollider2D boxCollider;
    Transform originalParent;
    bool isAtopPlug = false;
    public bool IsAtopPlug => isAtopPlug;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        originalParent = transform.parent.parent.parent;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        bool isMovingPlatform = ((1 << other.gameObject.layer) & movingPlatformsLayer.value) != 0; ;
        if (isMovingPlatform)
        {
            transform.parent.parent.SetParent(other.transform);
            PlugController plugController = other.GetComponent<PlugController>();
            if (plugController) isAtopPlug = true;
            else isAtopPlug = false;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        bool isMovingPlatform = LayerMaskExtensions.Contains(movingPlatformsLayer, gameObject.layer);
        if (isMovingPlatform)
        {
            if (transform.parent.parent.parent != other.transform)
            {
                transform.parent.parent.SetParent(other.transform);
                PlugController plugController = other.GetComponent<PlugController>();
                if (plugController) isAtopPlug = true;
                else isAtopPlug = false;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (transform.parent.parent.parent == other.transform)
        {
            if (!other.gameObject.activeInHierarchy || !other.enabled) return;
            if (transform.parent.parent == null || !transform.parent.parent.gameObject.activeInHierarchy) return;
            ContactFilter2D filter = new();
            filter.SetLayerMask(movingPlatformsLayer);
            filter.useTriggers = true;
            Collider2D[] foundColliders = new Collider2D[5];
            boxCollider.Overlap(filter, foundColliders);
            foreach (var collider in foundColliders)
            {
                if (collider == null) break;
                if (collider.transform == other.transform) return;
            }
            transform.parent.parent.SetParent(originalParent);
            PlugController plugController = other.GetComponent<PlugController>();
            if (plugController) isAtopPlug = false;
        }
    }
}
