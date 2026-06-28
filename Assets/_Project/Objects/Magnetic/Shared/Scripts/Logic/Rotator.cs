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

        public void RotateTo(Vector2 destination)
        {
            Vector2 currentPosition = transform.position;
            Vector2 direction = destination - currentPosition;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float snappedAngle = Mathf.Round(angle / 90f) * 90f;
            SetCalculatedRotation(Quaternion.Euler(0, 0, snappedAngle));
        }

        public void ResetRotation()
        {
            SetCalculatedRotation(originalRotation);
        }

        public void SetCalculatedRotation(Quaternion newRotation)
        {
            Quaternion calculatedNewRotation = CalculateRotation(newRotation);
            if (transform.localRotation.eulerAngles == calculatedNewRotation.eulerAngles) return;
            CancelChildrenRotation();
            transform.localRotation = calculatedNewRotation;
        }

        Quaternion CalculateRotation(Quaternion rotation)
        {
            return Quaternion.Euler(rotation.eulerAngles - transform.parent.rotation.eulerAngles);
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