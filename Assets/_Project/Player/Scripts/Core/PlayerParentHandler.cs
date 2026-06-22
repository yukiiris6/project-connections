using System.Linq;
using Unity.Cinemachine;
using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.Shared;

namespace ProjectConnections.Player
{
    public class PlayerParentHandler : MonoBehaviour
    {
        [SerializeField, Required] LayerMask dynamicSolidLayer;
        [SerializeField, Required] BoxCollider2D feetCollider;
        [SerializeField, Required] Transform playerTransform;

        Transform originalParent;

        void Awake()
        {
            originalParent = playerTransform.parent;
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            bool isDinamicSolid = IsDynamicSolid(other.gameObject.layer);
            if (isDinamicSolid && playerTransform.parent != other.transform)
            {
                playerTransform.SetParent(other.transform);
            }
        }

        void OnTriggerStay2D(Collider2D other)
        {
            bool isDinamicSolid = IsDynamicSolid(other.gameObject.layer);
            if (isDinamicSolid && playerTransform.parent != other.transform)
            {
                playerTransform.SetParent(other.transform);
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (playerTransform.parent == other.transform)
            {
                if (!other.gameObject.activeInHierarchy || !other.enabled) return;
                UnparentPlayer();
            }
        }

        bool IsDynamicSolid(int layer)
        {
            return LayerMaskExtensions.Contains(dynamicSolidLayer, layer);
        }

        void UnparentPlayer()
        {
            if (transform.parent == null) return;
            playerTransform.SetParent(originalParent);
        }
    }
}
