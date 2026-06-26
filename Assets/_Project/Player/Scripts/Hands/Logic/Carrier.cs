using UnityEngine;
using Sirenix.OdinInspector;
using System;
using ProjectConnections.ObjectShared;

namespace ProjectConnections.Player
{
    public class Carrier : MonoBehaviour
    {
        [SerializeField, Required] Transform carryAnchor;
        [SerializeField, Required] Transform centerAnchor;
        [SerializeField, Required] PlayerMovement playerMovement;
        [SerializeField, Required] LayerMask floorLayer;
        [SerializeField] Vector2 BoxcastSize = new(1f, 1f);
        [SerializeField] float throwStrength = 3f;
        [SerializeField] float throwHeight = 3f;
        [SerializeField] float releaseDistance = 2f;
        [SerializeField] float nearPlayerThreshold = .5f;
        [SerializeField] float minDistanceFromWall = .25f;

        public event Action<bool> OnCarryChanged;

        CarriableObject carryingObject;
        Vector2 lastXDirection = Vector2.zero;

        void Update()
        {
            FaceForward();
        }

        public void SetCarryingObject(CarriableObject newCarriableObject)
        {
            carryingObject = newCarriableObject;
            if (newCarriableObject != null)
            {
                lastXDirection = Vector2.zero;
                carryingObject.Carry(transform);
                carryingObject.transform.position = carryAnchor.position;
                carryingObject.OnCarryChanged += HandleCarryChanged;
                OnCarryChanged?.Invoke(true);
            }
        }

        public bool ThrowObject()
        {
            float xDirection = playerMovement.LastFacedLeft ? -1 : 1;
            float distance = GetDistanceToWall(Vector2.up);
            if (distance < carryAnchor.localPosition.y) return false;

            carryingObject.Throw(xDirection, throwHeight, throwStrength);
            carryingObject = null;
            OnCarryChanged?.Invoke(false);
            return true;
        }

        public void Drop()
        {
            float xDirection = playerMovement.LastFacedLeft ? -1 : 1;
            Vector2 vectorDirection = new(xDirection, 0f);

            float distance = GetDistanceToWall(vectorDirection);
            Vector2 dropLocation = GetDropLocation(xDirection, distance);
            Vector2 newPlayerPosition = GetNewPlayerPosition(xDirection, distance);

            transform.position = newPlayerPosition;
            carryingObject.Drop(dropLocation);

            carryingObject = null;
            OnCarryChanged?.Invoke(false);
        }

        public CarriableObject GetObject()
        {
            return carryingObject;
        }

        void HandleCarryChanged(bool value)
        {
            if (!value) carryingObject = null;
            OnCarryChanged?.Invoke(false);
        }

        Vector2 GetDropLocation(float xDirection, float distance)
        {
            Vector2 direction = new(xDirection, 0f);

            float appliedReleaseDistance = releaseDistance;

            if (distance < releaseDistance)
            {
                appliedReleaseDistance = distance - minDistanceFromWall;
            }

            float releaseX = centerAnchor.position.x + (xDirection * appliedReleaseDistance);
            return new(releaseX, centerAnchor.position.y);
        }

        float GetDistanceToWall(Vector2 direction)
        {
            RaycastHit2D hit = Physics2D.BoxCast(
                centerAnchor.position,
                BoxcastSize,
                0f,
                direction,
                float.MaxValue,
                floorLayer
            );

            return hit ? hit.distance : float.MaxValue;
        }

        Vector2 GetNewPlayerPosition(float xDirection, float distanceToWall)
        {
            if (distanceToWall < nearPlayerThreshold)
            {
                float signedMinDistanceFromWall = minDistanceFromWall * xDirection;
                float amountToPush = GetAmountToPush(distanceToWall);
                float signedPushDistance = amountToPush * xDirection;
                return new(transform.position.x - signedMinDistanceFromWall - signedPushDistance, transform.position.y);
            }

            return transform.position;
        }

        float GetAmountToPush(float distance)
        {
            float normalizedDistance = Mathf.Clamp(distance, 0, releaseDistance);
            float wallFactor = 1f - (normalizedDistance / releaseDistance);
            return releaseDistance * wallFactor;
        }

        void FaceForward()
        {
            if (carryingObject != null)
            {
                Vector2 newXDirection = playerMovement.LastFacedLeft ? Vector2.left : Vector2.right;
                if (newXDirection != lastXDirection)
                {
                    carryingObject.transform.right = newXDirection;
                    lastXDirection = newXDirection;
                }
            }
        }
    }
}
