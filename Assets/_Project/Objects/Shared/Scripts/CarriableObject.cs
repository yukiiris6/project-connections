using System;
using ProjectConnections.Magnetic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ProjectConnections.ObjectShared
{
    public class CarriableObject : MonoBehaviour
    {
        [SerializeField, Required] ObjectType objectType;
        [SerializeField, Required] Rigidbody2D myRigidbody;
        [SerializeField, Required] Collider2D myCollider;
        [SerializeField, Required] ObjectSoundPlayer objectSoundPlayer;
        [SerializeField, Required] Collider2D myTriggerVolume;

        public event Action<bool> OnCarryChanged;
        public event Action<bool> CarryTriggerChanged;
        public ObjectType ObjectType => objectType;
        public bool IsBeingCarried { get; private set; }
        public bool TriggerIsActive => myTriggerVolume.enabled;

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
            myRigidbody.linearVelocity = Vector2.zero;
            myRigidbody.bodyType = RigidbodyType2D.Kinematic;
            OnCarryChanged?.Invoke(true);
        }

        public void Throw(Vector2 throwDirection)
        {
            if (!CanThrow()) return;
            RestoreObject();
            myRigidbody.AddRelativeForce(throwDirection, ForceMode2D.Impulse);
            objectSoundPlayer.PlayThrowSFX();
            OnCarryChanged?.Invoke(false);
        }

        public void Drop(Vector2 dropPosition)
        {
            RestoreObject();
            transform.position = dropPosition;
            objectSoundPlayer.PlayDropSFX();
            OnCarryChanged?.Invoke(false);
        }

        public void LetGo()
        {
            RestoreObject();
            objectSoundPlayer.PlayDropSFX();
            OnCarryChanged?.Invoke(false);
        }

        public void ToggleTrigger(bool enable)
        {
            myTriggerVolume.enabled = enable;
            CarryTriggerChanged?.Invoke(enable);
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