using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class HintTimer : MonoBehaviour
{
    [SerializeField] float threshold = 60f;

    CanvasGroup canvasGroup;
    float timer = 0;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > threshold) canvasGroup.alpha = 1;
    }
}
