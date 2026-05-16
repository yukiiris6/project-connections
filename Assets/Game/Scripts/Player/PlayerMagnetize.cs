using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LineRenderer))]
public class PlayerMagnetize : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask magneticLayer;
    [SerializeField] Color magneticLineColor;
    [SerializeField] Color nonMagneticLineColor;

    GameObject selectedObject;
    Camera mainCamera;
    LineRenderer lineRenderer;
    PlugController magnetizedPlug;

    bool isAiming = false;

    #region Unity Lifecycle
    void Awake()
    {
        mainCamera = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 2;
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
    }

    void Update()
    {
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

        Vector2 origin = transform.position;
        Vector2 hitPosition = RaycastLaser(origin);

        ShowLaser(origin, hitPosition);
    }

    Vector2 RaycastLaser(Vector2 origin)
    {
        Vector2 mouseWorldPosition = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        int layermaskExceptPlayer = ~playerLayer.value;
        Vector2 direction = (mouseWorldPosition - origin).normalized;
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, float.MaxValue, layermaskExceptPlayer);
        selectedObject = hit.collider.gameObject;
        return hit.point;
    }

    void ProcessMagnet()
    {
        PlugController plugController = selectedObject.GetComponent<PlugController>();

        if (plugController != null)
        {
            ApplyMagnet(plugController);
        }
        else
        {
            CancelMagnet();
        }

        isAiming = false;
        HideLaser();
    }

    void ApplyMagnet(PlugController plugController)
    {
        CancelMagnet();
        if (plugController == magnetizedPlug) return;
        if (magnetizedPlug != null && magnetizedPlug.IsMoving) return;
        magnetizedPlug = plugController;
        plugController.Magnetize(transform.position);
    }

    void CancelMagnet()
    {
        if (magnetizedPlug == null) return;
        if (magnetizedPlug.IsMoving) return;
        magnetizedPlug.CancelMagnetism();
        magnetizedPlug = null;
    }
    #endregion

    #region Visuals
    Color GetLaserColor()
    {
        bool isMagnetic = ((1 << selectedObject.layer) & magneticLayer.value) != 0;
        return isMagnetic ? magneticLineColor : nonMagneticLineColor;
    }

    void ShowLaser(Vector2 origin, Vector2 hitPosition)
    {
        Color color = GetLaserColor();

        lineRenderer.SetPosition(0, origin);
        lineRenderer.SetPosition(1, hitPosition);

        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
        lineRenderer.enabled = true;
    }

    void HideLaser()
    {
        lineRenderer.enabled = false;
    }
    #endregion
}
