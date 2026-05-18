using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerAnimator))]
public class PlayerMagnetize : MonoBehaviour
{
    [SerializeField] Transform aimingTransform;
    [SerializeField] float laserThickness = 1f;
    [SerializeField] LineRenderer leftLineRenderer;
    [SerializeField] LineRenderer rightLineRenderer;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask magneticLayer;
    [SerializeField] Color magneticLineColor;
    [SerializeField] Color nonMagneticLineColor;

    Vector2 mouseWorldPosition;
    GameObject selectedObject;
    Camera mainCamera;
    PlugController magnetizedPlug;
    PlayerAnimator playerAnimator;

    bool isAiming = false;

    #region Unity Lifecycle
    void Awake()
    {
        mainCamera = Camera.main;
        playerAnimator = GetComponent<PlayerAnimator>();
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
        if (value.isPressed) isAiming = true;
        else ProcessMagnet();
    }

    void OnCancelMagnetism(InputValue value)
    {
        CancelMagnet();
    }

    void ProcessAiming()
    {
        if (!isAiming) return;

        FaceArmsToMouse();
        SelectFromRaycastCircle(aimingTransform.position, aimingTransform.right, laserThickness);
        RaycastLaser(leftLineRenderer);
        RaycastLaser(rightLineRenderer);
    }

    void SelectFromRaycastCircle(Vector2 origin, Vector2 direction, float laserThickness)
    {
        int layermaskExceptPlayer = ~playerLayer.value;
        RaycastHit2D hit = Physics2D.CircleCast(origin, laserThickness, direction, float.MaxValue, layermaskExceptPlayer);
        selectedObject = hit.collider.gameObject;
    }

    void ProcessMagnet()
    {
        PlugController plugController = selectedObject.GetComponent<PlugController>();

        if (plugController != null) ApplyMagnet(plugController);

        isAiming = false;
        playerAnimator.SetIsAiming(false);

        HideLaser(leftLineRenderer);
        HideLaser(rightLineRenderer);
    }

    void ApplyMagnet(PlugController plugController)
    {
        CancelMagnet();
        if (plugController == magnetizedPlug) return;
        if (magnetizedPlug != null && magnetizedPlug.IsMoving) return;
        magnetizedPlug = plugController;
        plugController.Magnetize(mouseWorldPosition, transform.position, false);
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
        playerAnimator.ControlAimingAnimation(isAiming, mouseDirection.x < 0);
    }

    void GetMousePosition()
    {
        if (!isAiming) return;
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
