using UnityEngine;

public class ShowHintTimer : MonoBehaviour
{
    [SerializeField] float threshold = 60f;
    [SerializeField] CanvasGroup canvasGroup;

    float timer = 0;

    void Awake()
    {
        canvasGroup.alpha = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > threshold) canvasGroup.alpha = 1;
    }
}
