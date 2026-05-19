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

    Vector2 mouseWorldPosition;
    GameObject selectedObject;
    Camera mainCamera;
    PlugController magnetizedPlug;
    PlayerAnimator playerAnimator;
    PlayerProgress playerProgress;

    public bool IsAiming { get; private set; } = false;

    #region Unity Lifecycle
    void Awake()
    {
        mainCamera = Camera.main;
        playerAnimator = GetComponent<PlayerAnimator>();
        playerProgress = GetComponent<PlayerProgress>();
        SetUpLaser(leftLineRenderer);
        SetUpLaser(rightLineRenderer);
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
        if (playerProgress.HasFinished) return;
        if (value.isPressed) IsAiming = true;
        else ProcessMagnet();
    }

    void OnCancelMagnetism(InputValue value)
    {
        if (playerProgress.HasFinished) return;
        CancelMagnet();
    }

    void ProcessAiming()
    {
        if (playerProgress.HasFinished)
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
    }

    void SelectFromRaycastCircle(Vector2 origin, Vector2 direction, float laserThickness)
    {
        int layermaskExceptPlayer = ~playerLayer.value;
        RaycastHit2D hit = Physics2D.CircleCast(origin, laserThickness, direction, float.MaxValue, layermaskExceptPlayer);

        SocketActivation newSocketActivation = hit.collider.gameObject.GetComponent<SocketActivation>();
        if (newSocketActivation != null) newSocketActivation.SetShowCircle(true, this);

        if (selectedObject != null)
        {
            SocketActivation socketActivation = selectedObject.GetComponent<SocketActivation>();
            if (socketActivation != null && newSocketActivation == null) socketActivation.SetShowCircle(false, this);
        }

        selectedObject = hit.collider.gameObject;
    }

    void ProcessMagnet()
    {
        PlugController plugController = selectedObject.GetComponent<PlugController>();
        if (plugController != null) ApplyPlugMagnet(plugController);

        SocketActivation socketActivation = selectedObject.GetComponent<SocketActivation>();
        if (socketActivation != null) ApplySocketMagnet(socketActivation);

        IsAiming = false;
        playerAnimator.SetIsAiming(false);

        HideLaser(leftLineRenderer);
        HideLaser(rightLineRenderer);
    }

    void ApplyPlugMagnet(PlugController plugController)
    {
        CancelMagnet();
        if (plugController == magnetizedPlug) return;
        if (magnetizedPlug != null && magnetizedPlug.IsMoving) return;
        magnetizedPlug = plugController;
        plugController.Magnetize(magnetAnchor.position);
    }

    void ApplySocketMagnet(SocketActivation socketActivation)
    {
        socketActivation.Magnetize();
        magnetizedPlug = null;
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
        bool isMagnetic = ((1 << selectedObject.layer) & magneticLayer.value) != 0;
        return isMagnetic ? magneticLineColor : nonMagneticLineColor;
    }
    #endregion
}
