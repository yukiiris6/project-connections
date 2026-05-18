using System.Collections;
using UnityEngine;

public class IrisWipeController : MonoBehaviour
{
    [SerializeField] Material irisMaterial;
    [SerializeField] Camera mainCamera;
    [SerializeField] float duration = 1.5f;

    RectTransform rectTransform;
    float currentRadius = 1f;

    public float Duration => duration;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void StartIrisOpen(Transform targetObject)
    {
        Vector2 screenPoint = mainCamera.WorldToScreenPoint(targetObject.position);
        rectTransform.position = screenPoint;
        StartCoroutine(AnimateIris(0, 1f));
    }

    public void StartIrisWipe(Transform targetObject)
    {
        Vector2 screenPoint = mainCamera.WorldToScreenPoint(targetObject.position);
        rectTransform.position = screenPoint;
        StartCoroutine(AnimateIris(1f, 0));
    }

    IEnumerator AnimateIris(float startRadius, float endRadius)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            currentRadius = Mathf.Lerp(startRadius, endRadius, elapsed / duration);
            irisMaterial.SetFloat("_Radius", currentRadius);
            yield return null;
        }
    }
}