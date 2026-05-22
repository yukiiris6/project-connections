using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class IrisWipeController : MonoBehaviour
{
    [SerializeField] Material irisMaterial;
    [SerializeField] float duration = 1.5f;

    Transform irisWipeTarget;
    Camera mainCamera;
    RectTransform rectTransform;

    public float Duration => duration;

    void GetDependencies()
    {
        if (irisWipeTarget != null && mainCamera != null && rectTransform != null) return;
        irisWipeTarget = FindFirstObjectByType<CenterAnchor>().transform;
        mainCamera = Camera.main;
        rectTransform = GetComponent<RectTransform>();
    }

    void Start()
    {
        irisMaterial.SetFloat("_Radius", .75f);
    }

    public void StartIrisOpen()
    {
        GetDependencies();
        CalculateMaximumRadius();
        Vector2 screenPoint = mainCamera.WorldToScreenPoint(irisWipeTarget.position);
        rectTransform.position = screenPoint;
        irisMaterial.SetFloat("_Radius", 0);
        irisMaterial.DOFloat(.75f, "_Radius", duration).SetUpdate(true).SetEase(Ease.Linear);
    }

    public void StartIrisWipe()
    {
        GetDependencies();
        CalculateMaximumRadius();
        Vector2 screenPoint = mainCamera.WorldToScreenPoint(irisWipeTarget.position);
        rectTransform.position = screenPoint;
        irisMaterial.SetFloat("_Radius", .75f);
        irisMaterial.DOFloat(0f, "_Radius", duration).SetUpdate(true).SetEase(Ease.Linear);
    }

    void CalculateMaximumRadius()
    {
        RectTransform canvasRect = rectTransform.parent as RectTransform;
        float canvasWidth = canvasRect.rect.width;
        float canvasHeight = canvasRect.rect.height;

        Vector2 targetViewportPosition = mainCamera.WorldToViewportPoint(irisWipeTarget.position);
        Vector2 targetCanvasPosition = new(
            targetViewportPosition.x * canvasWidth,
            targetViewportPosition.y * canvasHeight
        );

        float distanceLeft = targetCanvasPosition.x;
        float distanceRight = canvasWidth - targetCanvasPosition.x;
        float distanceDown = targetCanvasPosition.y;
        float distanceUp = canvasHeight - targetCanvasPosition.y;

        float farthestDistance = Mathf.Max(distanceLeft, distanceRight, distanceDown, distanceUp);

        rectTransform.offsetMin = new(-farthestDistance, -farthestDistance);
        rectTransform.offsetMax = new(farthestDistance, farthestDistance);
    }
}