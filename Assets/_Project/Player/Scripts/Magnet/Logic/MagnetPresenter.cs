using UnityEngine;

public class MagnetPresenter : MonoBehaviour
{
    [SerializeField] LineRenderer[] lineRenderers;
    [SerializeField] Transform aimingTransform;
    [SerializeField] Color magneticLineColor;
    [SerializeField] Color nonMagneticLineColor;

    CursorPresenter cursorController;

    void Awake()
    {
        SetUpLaser();
    }

    void Start()
    {
        cursorController = OverlaySystems.Instance.CursorPresenter;
    }


    public void UpdateVisuals(GameObject targetObject)
    {
        ShowLaser(targetObject);
        UpdateAimingCursor(targetObject);
    }

    public void ResetVisuals()
    {
        HideLaser();
        ResetCursor();
    }

    void SetUpLaser()
    {
        foreach (var lineRenderer in lineRenderers)
        {
            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = 0.05f;
            lineRenderer.endWidth = 0.05f;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        }
    }

    void ShowLaser(GameObject targetObject)
    {
        foreach (var lineRenderer in lineRenderers)
        {
            Vector2 origin = lineRenderer.transform.position;
            Vector2 direction = lineRenderer.transform.up;
            int allLayersExceptPlayer = ~gameObject.layer;

            Vector2 targetPoint;
            if (targetObject == null)
            {
                RaycastHit2D hit = Physics2D.Raycast(origin, direction, float.MaxValue, allLayersExceptPlayer);
                targetPoint = hit.point;
            }
            else
            {
                Vector2 targetOffset = (Vector2)targetObject.transform.position - origin;
                float distanceToTarget = Vector2.Dot(targetOffset, direction);
                targetPoint = origin + (direction * distanceToTarget);
            }

            Color color = GetLaserColor(targetObject);
            lineRenderer.SetPosition(0, origin);
            lineRenderer.SetPosition(1, targetPoint);
            lineRenderer.startColor = color;
            lineRenderer.endColor = color;
            lineRenderer.enabled = true;
        }
    }

    void HideLaser()
    {
        foreach (var lineRenderer in lineRenderers)
        {
            lineRenderer.enabled = false;
        }
    }

    Color GetLaserColor(GameObject targetObject)
    {
        if (targetObject == null) return nonMagneticLineColor;
        bool isMagnetic = targetObject.CompareTag("Magnetic");
        return isMagnetic ? magneticLineColor : nonMagneticLineColor;
    }

    void UpdateAimingCursor(GameObject targetObject)
    {
        if (targetObject == null)
        {
            cursorController.ChangeToAimingCursor();
            return;
        }
        bool isMagnetic = targetObject.CompareTag("Magnetic");
        if (isMagnetic) cursorController.ChangeToMagnetizeCursor(aimingTransform.right);
        else cursorController.ChangeToAimingCursor();
    }

    void ResetCursor()
    {
        cursorController.ChangeToNormalCursor();
    }
}