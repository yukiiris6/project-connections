using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;

namespace ProjectConnections.Player
{
    public class MouseFollower : MonoBehaviour
    {
        [Header("References")]
        [SerializeField, Required] Transform playerTransform;

        [Header("Settings")]
        [SerializeField, Required] float maxDistance = 3.5f;

        private Camera mainCamera;

        void Awake()
        {
            mainCamera = Camera.main;
        }

        void Update()
        {
            FollowMouse();
        }

        void FollowMouse()
        {
            Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
            mouseWorldPos.z = 0f;

            Vector3 direction = mouseWorldPos - playerTransform.position;
            float currentDistance = direction.magnitude;

            if (currentDistance > maxDistance)
            {
                transform.position = playerTransform.position + direction.normalized * maxDistance;
            }
            else
            {
                transform.position = mouseWorldPos;
            }
        }
    }
}
