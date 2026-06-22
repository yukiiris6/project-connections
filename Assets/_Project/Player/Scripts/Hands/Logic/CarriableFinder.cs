using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.Shared;
using System;

namespace ProjectConnections.Player
{
    public class CarriableFinder : MonoBehaviour
    {
        public event Action<CarriableObject> OnObjectFound;

        CarriableObject foundCarriableObject;

        void OnTriggerEnter2D(Collider2D other)
        {
            var carriableObject = other.gameObject.GetComponent<CarriableObject>();
            if (carriableObject != null)
            {
                if (foundCarriableObject != null)
                {
                    foundCarriableObject.CarryOnTriggerChanged -= HandleOnTriggerChanged;
                }

                if (carriableObject.ShouldCarryOnTrigger)
                {
                    OnObjectFound?.Invoke(carriableObject);
                }
                else
                {
                    foundCarriableObject = carriableObject;
                    carriableObject.CarryOnTriggerChanged += HandleOnTriggerChanged;
                }
            }
        }

        void OnTriggerExit2D(Collider2D other)
        {
            var carriableObject = other.gameObject.GetComponent<CarriableObject>();
            if (carriableObject != null)
            {
                if (foundCarriableObject != null && foundCarriableObject == carriableObject)
                {
                    foundCarriableObject.CarryOnTriggerChanged -= HandleOnTriggerChanged;
                    foundCarriableObject = null;
                }
            }
        }

        void HandleOnTriggerChanged(bool value)
        {
            if (value)
            {
                OnObjectFound?.Invoke(foundCarriableObject);
                foundCarriableObject.CarryOnTriggerChanged -= HandleOnTriggerChanged;
                foundCarriableObject = null;
            }
        }
    }
}
