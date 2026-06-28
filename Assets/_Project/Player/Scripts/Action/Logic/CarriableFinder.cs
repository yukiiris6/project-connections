using UnityEngine;
using Sirenix.OdinInspector;
using System;
using ProjectConnections.ObjectShared;

namespace ProjectConnections.Player
{
    public class CarriableFinder : MonoBehaviour
    {
        public event Action<CarriableObject> OnObjectFound;

        CarriableObject foundCarriableObject;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Carriable")) return;

            var carriableTrigger = other.gameObject.GetComponent<CarriableTrigger>();
            if (carriableTrigger == null) return;

            var carriableObject = carriableTrigger.GetCarriableObject();
            foundCarriableObject = carriableObject;
            OnObjectFound?.Invoke(carriableObject);
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Carriable")) return;

            var carriableTrigger = other.gameObject.GetComponent<CarriableTrigger>();
            if (carriableTrigger == null) return;
            if (foundCarriableObject == null) return;

            var carriableObject = carriableTrigger.GetCarriableObject();
            if (foundCarriableObject != carriableObject) return;

            foundCarriableObject.OnCarryChanged -= HandleOnCarryChanged;
            foundCarriableObject = null;
        }


        void HandleOnCarryChanged(bool value)
        {
            if (value) return;
            if (foundCarriableObject == null) return;
            OnObjectFound?.Invoke(foundCarriableObject);
            foundCarriableObject.OnCarryChanged -= HandleOnCarryChanged;
            foundCarriableObject = null;
        }
    }
}
