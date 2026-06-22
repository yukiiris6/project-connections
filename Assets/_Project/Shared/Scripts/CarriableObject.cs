using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class CarriableObject : MonoBehaviour
{
    [field: SerializeField] public ObjectType ObjectType { get; private set; }
    [field: SerializeField, Required] public bool ShouldCarryOnTrigger { get; private set; }
    [SerializeField] Rigidbody2D myRigidbody;

    public event Action<bool> OnCarryChanged;
    public event Action<bool> CarryOnTriggerChanged;

    Transform originalParent;
    RigidbodyType2D originalBodyType;

    void Awake()
    {
        originalParent = transform.parent;
        originalBodyType = myRigidbody.bodyType;
    }

    public void Carry(Transform carrier)
    {
        myRigidbody.bodyType = RigidbodyType2D.Kinematic;
        transform.parent = carrier;
        OnCarryChanged?.Invoke(true);
    }

    public void Throw(float xDirection, float height, float strength)
    {
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

    void RestoreObject()
    {
        SetCarryOnTrigger(false);
        myRigidbody.bodyType = originalBodyType;
        transform.parent = originalParent;
    }
}