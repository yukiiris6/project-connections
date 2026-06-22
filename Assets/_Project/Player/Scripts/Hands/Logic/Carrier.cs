using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.Shared;
using System;

namespace ProjectConnections.Player
{
    public class Carrier : MonoBehaviour
    {
        [SerializeField, Required] Transform carryAnchor;
        [SerializeField, Required] Transform centerAnchor;
        [SerializeField] PlayerMovement playerMovement;
        [SerializeField] LayerMask floorLayer;
        [SerializeField] Vector2 BoxcastSize = new(1f, 1f);
        [SerializeField] float throwStrength = 3f;
        [SerializeField] float throwHeight = 3f;
        [SerializeField] float releaseDistance = 2f;
        [SerializeField] float nearPlayerThreshold = .5f;
        [SerializeField] float minDistanceFromWall = .25f;

        public event Action<bool> OnCarryChanged;

        CarriableObject carryingObject;

        public void SetCarryingObject(CarriableObject newCarriableObject)
        {
            carryingObject = newCarriableObject;
            if (newCarriableObject != null)
            {
                carryingObject.Carry(transform);
                carryingObject.transform.position = carryAnchor.position;
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

            Vector2 dropLocation = GetDropLocation(xDirection);
            Vector2 newPlayerPosition = GetNewPlayerPosition(xDirection, dropLocation);

            transform.position = newPlayerPosition;
            carryingObject.Drop(dropLocation);

            carryingObject = null;
            OnCarryChanged?.Invoke(false);
        }

        Vector2 GetDropLocation(float xDirection)
        {
            Vector2 direction = new(xDirection, 0f);

            float distance = GetDistanceToWall(direction);
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

        Vector2 GetNewPlayerPosition(float xDirection, Vector2 dropLocation)
        {
            float playerDistanceToDrop = Vector2.Distance(centerAnchor.position, dropLocation);

            if (playerDistanceToDrop < nearPlayerThreshold)
            {
                float signedMinDistanceFromWall = minDistanceFromWall * xDirection;
                return new(transform.position.x - signedMinDistanceFromWall - (xDirection * releaseDistance), transform.position.y);
            }

            return transform.position;
        }
    }
}
