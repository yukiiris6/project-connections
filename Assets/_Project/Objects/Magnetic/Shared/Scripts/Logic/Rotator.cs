using UnityEngine;

namespace ProjectConnections.Magnetic
{
    public class Rotator : MonoBehaviour
    {
        Quaternion originalRotation;

        void Awake()
        {
            originalRotation = transform.rotation;
        }

        public void RotateTowardsTarget(Vector2 targetPosition)
        {
            Vector2 currentPosition = transform.position;
            Vector2 direction = targetPosition - currentPosition;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float snappedAngle = Mathf.Round(angle / 90f) * 90f;
            SetRotation(Quaternion.Euler(0, 0, snappedAngle));
        }

        public void ResetRotation()
        {
            SetRotation(originalRotation);
        }

        public void SetRotation(Quaternion newRotation)
        {
            CancelChildrenRotation();
            transform.localRotation = Quaternion.Euler(newRotation.eulerAngles - transform.parent.rotation.eulerAngles);
        }

        void CancelChildrenRotation()
        {
            if (transform.childCount <= 0) return;
            foreach (Transform child in transform)
            {
                if (!child.CompareTag("Player")) continue;
                child.SetParent(null);
                child.rotation = Quaternion.identity;
            }
        }
    }
}