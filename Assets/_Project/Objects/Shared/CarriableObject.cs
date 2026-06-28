using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectConnections.ObjectShared
{
    public class CarriableObject : MonoBehaviour
    {
        [SerializeField, Required] ObjectType objectType;
        [SerializeField, Required] Rigidbody2D myRigidbody;
        [SerializeField, Required] Collider2D myCollider;
        [SerializeField, Required] bool shouldCarryOnTrigger;

        public event Action<bool> OnCarryChanged;
        public event Action<bool> CarryOnTriggerChanged;
        public bool ShouldCarryOnTrigger => shouldCarryOnTrigger;
        public bool IsBeingCarried { get; private set; }
        public ObjectType ObjectType => objectType;

        Transform originalParent;
        RigidbodyType2D originalBodyType;

        void Awake()
        {
            originalParent = transform.parent;
            originalBodyType = myRigidbody.bodyType;
        }

        void Update()
        {
            // ValidateBodyType();
        }

        public void Carry(Transform carrier)
        {
            myCollider.enabled = false;
            IsBeingCarried = true;
            transform.parent = carrier;
            OnCarryChanged?.Invoke(true);
            myRigidbody.bodyType = RigidbodyType2D.Kinematic;
        }

        public void Throw(Vector2 throwDirection)
        {
            if (!CanThrow()) return;
            RestoreObject();
            myRigidbody.AddRelativeForce(throwDirection, ForceMode2D.Impulse);
            OnCarryChanged?.Invoke(false);
        }

        public void Drop(Vector2 dropPosition)
        {
            RestoreObject();
            transform.position = dropPosition;
            OnCarryChanged?.Invoke(false);
        }

        public void LetGo()
        {
            RestoreObject();
            OnCarryChanged?.Invoke(false);
        }

        public void SetCarryOnTrigger(bool value)
        {
            shouldCarryOnTrigger = value;
            CarryOnTriggerChanged?.Invoke(value);
        }

        public bool CanThrow()
        {
            return originalBodyType == RigidbodyType2D.Dynamic;
        }

        void ValidateBodyType()
        {
            if (!IsBeingCarried) return;
            myRigidbody.bodyType = RigidbodyType2D.Kinematic;
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