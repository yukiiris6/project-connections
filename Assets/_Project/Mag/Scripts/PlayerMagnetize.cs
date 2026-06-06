using System.Collections;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerAnimator))]
[RequireComponent(typeof(PlayerProgress))]
public class PlayerMagnetize : MonoBehaviour
{
    [SerializeField] Transform magnetAnchor;
    [SerializeField] Transform aimingTransform;
    [SerializeField] LineRenderer leftLineRenderer;
    [SerializeField] LineRenderer rightLineRenderer;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask magneticLayer;
    [SerializeField] Color magneticLineColor;
    [SerializeField] Color nonMagneticLineColor;
    [SerializeField] float laserThickness = 1f;
    [SerializeField] CinemachineTargetGroup targetGroup;
    [SerializeField] Transform mouseAnchor;

    Vector2 mouseWorldPosition;
    GameObject selectedObject;
    Camera mainCamera;
    PlugController magnetizedPlug;
    PlayerAnimator playerAnimator;
    CursorController cursorController;
    LevelManager levelManager;

    public bool IsAiming { get; private set; } = false;
    int mouseIndex = 0;

    #region Unity Lifecycle
    void Awake()
    {
        mainCamera = Camera.main;
        playerAnimator = GetComponent<PlayerAnimator>();
        SetUpLaser(leftLineRenderer);
        SetUpLaser(rightLineRenderer);
    }

    void Start()
    {
        cursorController = OverlayCanvas.Instance.CursorController;
        levelManager = GlobalSystems.Instance.LevelManager;
        mouseIndex = targetGroup.FindMember(mouseAnchor);
    }

    void Update()
    {
        GetMousePosition();
        ProcessAiming();
    }
    #endregion

    #region Magnetism
    void OnMagnetize(InputValue value)
    {
        if (levelManager.IsLoading) return;
        if (value.isPressed)
        {
            IsAiming = true;
            targetGroup.Targets[mouseIndex].Weight = .5f;
        }
        else
        {
            ProcessMagnet();
            targetGroup.Targets[mouseIndex].Weight = 0f;
        }
    }

    void OnCancelMagnetism(InputValue value)
    {
        if (levelManager.IsLoading) return;
        CancelMagnet();
    }

    void ProcessAiming()
    {
        if (levelManager.IsLoading)
        {
            HideLaser(leftLineRenderer);
            HideLaser(rightLineRenderer);
            return;
        }

        if (!IsAiming) return;

        FaceArmsToMouse();
        SelectFromRaycastCircle(aimingTransform.position, aimingTransform.right, laserThickness);
        RaycastLaser(leftLineRenderer);
        RaycastLaser(rightLineRenderer);
        ChangeCursor();
    }

    void SelectFromRaycastCircle(Vector2 origin, Vector2 direction, float laserThickness)
    {
        int layermaskExceptPlayer = ~playerLayer.value;
        RaycastHit2D hit = Physics2D.CircleCast(origin, laserThickness, direction, float.MaxValue, layermaskExceptPlayer);

        SocketController newSocketActivation = hit.collider.gameObject.GetComponent<SocketController>();
        if (newSocketActivation != null) newSocketActivation.SetShowCircle(true, this);

        if (selectedObject != null)
        {
            SocketController socketActivation = selectedObject.GetComponent<SocketController>();
            if (socketActivation != null && newSocketActivation == null) socketActivation.SetShowCircle(false, this);
        }

        selectedObject = hit.collider.gameObject;
    }

    void ProcessMagnet()
    {
        if (selectedObject == null) return;

        SocketController socketActivation = selectedObject.GetComponent<SocketController>();
        if (socketActivation != null) ApplySocketMagnet(socketActivation);

        PlugController plugController = selectedObject.GetComponent<PlugController>();
        if (!plugController)
        {
            var plugContainer = selectedObject.GetComponent<PlugContainer>();
            if (plugContainer != null) plugController = plugContainer.PlugController;
        }

        IsAiming = false;
        playerAnimator.SetIsAiming(false);
        HideLaser(leftLineRenderer);
        HideLaser(rightLineRenderer);
        cursorController.ChangeToNormalCursor();

        bool isAimingAtPlug = plugController != null;
        bool hasMagnetizedPlug = magnetizedPlug != null;
        bool aimedPlugIsNotInSocket = isAimingAtPlug && !plugController.IsInSocket;
        if (hasMagnetizedPlug && aimedPlugIsNotInSocket) CancelMagnet();
        if (isAimingAtPlug) ApplyPlugMagnet(plugController);
    }

    void ApplyPlugMagnet(PlugController plugController)
    {
        if (magnetizedPlug && magnetizedPlug.IsMoving) return;
        magnetizedPlug = plugController;
        plugController.Magnetize(magnetAnchor.position);
    }

    void ApplySocketMagnet(SocketController socketActivation)
    {
        var plugInSocket = socketActivation.Magnetize();
        if (magnetizedPlug == plugInSocket) magnetizedPlug = null;
    }

    void CancelMagnet()
    {
        if (magnetizedPlug == null) return;
        if (magnetizedPlug.IsMoving) return;
        magnetizedPlug.CancelMagnetism();
        magnetizedPlug = null;
    }
    #endregion

    #region Logic
    void FaceArmsToMouse()
    {
        Vector2 mouseDirection = mouseWorldPosition - (Vector2)aimingTransform.position;
        aimingTransform.right = mouseDirection;
        playerAnimator.ControlAimingAnimation(IsAiming, mouseDirection.x < 0);
    }

    void GetMousePosition()
    {
        if (!IsAiming) return;
        mouseWorldPosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
    }
    #endregion

    #region Visuals
    void SetUpLaser(LineRenderer lineRenderer)
    {
        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
    }

    void RaycastLaser(LineRenderer lineRenderer)
    {
        Vector2 origin = lineRenderer.transform.position;
        Vector2 direction = lineRenderer.transform.up;
        int layermaskExceptPlayer = ~playerLayer.value;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, float.MaxValue, layermaskExceptPlayer);
        ShowLaser(lineRenderer, hit.point);
    }

    void ShowLaser(LineRenderer lineRenderer, Vector2 hitPosition)
    {
        Color color = GetLaserColor();
        Vector2 origin = lineRenderer.transform.position;

        lineRenderer.SetPosition(0, origin);
        lineRenderer.SetPosition(1, hitPosition);

        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.enabled = true;
    }

    void HideLaser(LineRenderer lineRenderer)
    {
        lineRenderer.enabled = false;
    }

    Color GetLaserColor()
    {
        bool isMagnetic = LayerMaskExtensions.Contains(magneticLayer, selectedObject.layer);
        return isMagnetic ? magneticLineColor : nonMagneticLineColor;
    }

    void ChangeCursor()
    {
        if (IsAiming)
        {
            bool isMagnetic = LayerMaskExtensions.Contains(magneticLayer, selectedObject.layer);
            if (isMagnetic) cursorController.ChangeToMagnetizeCursor(aimingTransform.right);
            else cursorController.ChangeToAimingCursor();
        }
    }
    #endregion
}
