using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.SceneUI
{
    public class ShowHintTimer : MonoBehaviour
    {
        [SerializeField, Required] float threshold = 60f;
        [SerializeField, Required] CanvasGroup canvasGroup;

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
}
