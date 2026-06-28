using System;
using System.Collections;
using DG.Tweening;
using ProjectConnections.Magnetic;
using Unity.Cinemachine;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;

namespace ProjectConnections.Player
{
    public class MagnetAiming : MonoBehaviour
    {
        [SerializeField, Required] MagnetPresenter magnetPresenter;
        [SerializeField, Required] CinemachineTargetGroup targetGroup;
        [SerializeField, Required] Transform centerTransform;
        [SerializeField, Required] Transform aimingTransform;
        [SerializeField, Required] Transform mouseTransform;
        [SerializeField, Required] LayerMask layersToIgnore;
        [SerializeField, Required] float laserThickness = 1f;
        [SerializeField, Required] string magneticTag = "Magnetic";

        public event Action<float> OnChangeFacing;
        bool isAiming;

        Camera mainCamera;
        GameObject targetObject;
        Magnetism targetMagneticObject;
        int targetGroupMouseIndex = 0;

        void Start()
        {
            mainCamera = Camera.main;
            targetGroupMouseIndex = targetGroup.FindMember(mouseTransform);
        }

        void Update()
        {
            ExecuteAiming();
        }

        public void Aim()
        {
            isAiming = true;
            targetGroup.Targets[targetGroupMouseIndex].Weight = 0.5f;
        }

        public void StopAiming()
        {
            isAiming = false;
            targetObject = null;
            targetGroup.Targets[targetGroupMouseIndex].Weight = 0f;
            magnetPresenter.ResetVisuals();
            DemagnetizeObject();
            ResetFacing();
        }

        void ExecuteAiming()
        {
            if (!isAiming) return;

            if (targetMagneticObject == null)
            {
                FaceMouse();
                targetObject = GetTarget();
                SetMagnetismModule();
            }
            else
            {
                FaceMagneticObject();
                MagnetizeObject();
            }

            magnetPresenter.UpdateVisuals(targetObject);
        }

        public GameObject GetTargetObject()
        {
            return targetObject;
        }

        GameObject GetTarget()
        {
            Vector2 origin = aimingTransform.position;
            Vector2 direction = aimingTransform.right;
            int allLayersExceptIgnored = ~layersToIgnore.value;

            RaycastHit2D hit = Physics2D.CircleCast(
                origin,
                laserThickness,
                direction,
                float.MaxValue,
                allLayersExceptIgnored
            );

            if (hit.collider && hit.collider.gameObject.CompareTag(magneticTag))
            {
                return hit.collider.gameObject;
            }
            return null;
        }

        void SetMagnetismModule()
        {
            if (targetObject == null) return;
            Magnetism magneticObject = targetObject.GetComponent<Magnetism>();
            if (magneticObject != null) targetMagneticObject = magneticObject;
        }

        void MagnetizeObject()
        {
            if (targetMagneticObject == null) return;
            targetMagneticObject.Magnetize(centerTransform.position);
        }

        void DemagnetizeObject()
        {
            if (targetMagneticObject == null) return;
            targetMagneticObject.Demagnetize();
            targetMagneticObject = null;
        }

        void FaceMouse()
        {
            Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            Vector2 mouseDirection = mouseWorldPosition - (Vector2)aimingTransform.position;
            aimingTransform.right = mouseDirection;
            OnChangeFacing?.Invoke(mouseDirection.x);
        }

        void ResetFacing()
        {
            aimingTransform.right = Vector2.right;
        }

        void FaceMagneticObject()
        {
            aimingTransform.right = targetObject.transform.position - aimingTransform.position;
            OnChangeFacing?.Invoke(aimingTransform.right.x);
        }
    }
}
