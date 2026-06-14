using DG.Tweening;
using UnityEngine;

public class CircleIrisWipe : MonoBehaviour
{
    [field: SerializeField] public float Duration { get; private set; } = 1.5f;
    [SerializeField] Material irisMaterial;
    [SerializeField] RectTransform rectTransform;

    Transform irisWipeTarget;
    Camera mainCamera;

    void GetDependencies()
    {
        if (irisWipeTarget != null && mainCamera != null && rectTransform != null) return;
        // irisWipeTarget = FindFirstObjectByType<CenterAnchor>().transform;
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
        irisMaterial.DOFloat(.75f, "_Radius", Duration).SetUpdate(true).SetEase(Ease.Linear);
    }

    public void StartIrisWipe()
    {
        GetDependencies();
        CalculateMaximumRadius();
        Vector2 screenPoint = mainCamera.WorldToScreenPoint(irisWipeTarget.position);
        rectTransform.position = screenPoint;
        irisMaterial.SetFloat("_Radius", .75f);
        irisMaterial.DOFloat(0f, "_Radius", Duration).SetUpdate(true).SetEase(Ease.Linear);
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