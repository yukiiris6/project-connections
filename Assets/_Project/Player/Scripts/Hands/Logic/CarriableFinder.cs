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
            var carriableObject = other.gameObject.GetComponent<CarriableObject>();
            if (carriableObject != null)
            {
                if (foundCarriableObject != null)
                {
                    foundCarriableObject.OnCarryChanged -= HandleOnCarryChanged;
                }

                if (carriableObject.ShouldCarryOnTrigger)
                {
                    OnObjectFound?.Invoke(carriableObject);
                }
                else
                {
                    foundCarriableObject = carriableObject;
                    carriableObject.OnCarryChanged += HandleOnCarryChanged;
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
                    foundCarriableObject.OnCarryChanged -= HandleOnCarryChanged;
                    foundCarriableObject = null;
                }
            }
        }

        void HandleOnCarryChanged(bool value)
        {
            if (!value && foundCarriableObject != null)
            {
                OnObjectFound?.Invoke(foundCarriableObject);
                foundCarriableObject.OnCarryChanged -= HandleOnCarryChanged;
                foundCarriableObject = null;
            }
        }
    }
}
