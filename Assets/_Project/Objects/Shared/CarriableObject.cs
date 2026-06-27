using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectConnections.ObjectShared
{
    public class CarriableObject : MonoBehaviour
    {
        [field: SerializeField, Required] public ObjectType ObjectType { get; private set; }
        [field: SerializeField, Required] public bool ShouldCarryOnTrigger { get; private set; }
        [SerializeField, Required] Rigidbody2D myRigidbody;
        [SerializeField, Required] Collider2D myCollider;

        public event Action<bool> OnCarryChanged;
        public event Action<bool> CarryOnTriggerChanged;
        public bool IsBeingCarried { get; private set; }
        Transform originalParent;
        RigidbodyType2D originalBodyType;

        void Awake()
        {
            originalParent = transform.parent;
            originalBodyType = myRigidbody.bodyType;
        }

        public void Carry(Transform carrier)
        {
            myCollider.enabled = false;
            myRigidbody.bodyType = RigidbodyType2D.Kinematic;
            transform.parent = carrier;
            IsBeingCarried = true;
            OnCarryChanged?.Invoke(true);
        }

        public void Throw(float xDirection, float height, float strength)
        {
            if (!CanThrow()) return;
            RestoreObject();
            Vector2 throwDirection = new(xDirection * strength, height);
            myRigidbody.AddRelativeForce(throwDirection, ForceMode2D.Impulse);
            OnCarryChanged?.Invoke(false);
        }

        public void Drop(Vector2 dropPosition)
        {
            RestoreObject();
            transform.position = dropPosition;
            OnCarryChanged?.Invoke(false);
        }

        public void SetCarryOnTrigger(bool value)
        {
            ShouldCarryOnTrigger = value;
            CarryOnTriggerChanged?.Invoke(value);
        }

        public void LetGo()
        {
            RestoreObject();
            OnCarryChanged?.Invoke(false);
        }

        public bool CanThrow()
        {
            return originalBodyType == RigidbodyType2D.Dynamic;
        }

        void RestoreObject()
        {
            myCollider.enabled = true;
            IsBeingCarried = false;
            myRigidbody.bodyType = originalBodyType;
            transform.parent = originalParent;
        }
    }
}