using System;
using System.Collections;
using DG.Tweening;
using ProjectConnections.Magnetism;
using ProjectConnections.Magnetism.Anchored;
using ProjectConnections.Magnetism.Docked;
using ProjectConnections.Magnetism.Modules;
using Unity.Cinemachine;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;

public class MagnetAiming : MonoBehaviour
{
    [SerializeField, Required] MagnetPresenter magnetPresenter;
    [SerializeField, Required] CinemachineTargetGroup targetGroup;
    [SerializeField, Required] Transform centerTransform;
    [SerializeField, Required] Transform aimingTransform;
    [SerializeField, Required] Transform mouseTransform;
    [SerializeField, Required] LayerMask playerLayer;
    [SerializeField, Required] float laserThickness = 1f;
    [SerializeField, Required] string magneticTag = "Magnetic";

    public event Action<float> OnChangeFacing;
    bool isAiming;

    Camera mainCamera;
    GameObject targetObject;
    MagnetismModule targetMagnetismModule;
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
        targetGroup.Targets[targetGroupMouseIndex].Weight = 0.5f;
        isAiming = true;
    }

    public void StopAiming()
    {
        targetGroup.Targets[targetGroupMouseIndex].Weight = 0f;
        isAiming = false;
        targetObject = null;
        DemagnetizeObject();
        magnetPresenter.ResetVisuals();
    }

    void ExecuteAiming()
    {
        if (!isAiming) return;

        if (targetMagnetismModule == null)
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
        int allLayersExceptPlayer = ~playerLayer.value;

        RaycastHit2D hit = Physics2D.CircleCast(
            origin,
            laserThickness,
            direction,
            float.MaxValue,
            allLayersExceptPlayer
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
        MagnetismModule magnetismModule = targetObject.GetComponent<MagnetismModule>();
        if (magnetismModule != null) targetMagnetismModule = magnetismModule;
    }

    void MagnetizeObject()
    {
        if (targetMagnetismModule == null) return;
        targetMagnetismModule.Magnetize(centerTransform.position);
    }

    void DemagnetizeObject()
    {
        if (targetMagnetismModule == null) return;
        targetMagnetismModule.Demagnetize();
        targetMagnetismModule = null;
    }

    void FaceMouse()
    {
        Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 mouseDirection = mouseWorldPosition - (Vector2)aimingTransform.position;
        aimingTransform.right = mouseDirection;
        OnChangeFacing?.Invoke(mouseDirection.x);
    }

    void FaceMagneticObject()
    {
        aimingTransform.right = targetObject.transform.position - aimingTransform.position;
        OnChangeFacing?.Invoke(aimingTransform.right.x);
    }
}
