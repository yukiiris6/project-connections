using UnityEngine;

namespace ProjectConnections.Magnetism
{
    public class PluggableCables : MonoBehaviour
    {
        [SerializeField] Transform cableStartPosition;
        [SerializeField] Transform cableEndPosition;
        [SerializeField] Color cableColor;

        LineRenderer lineRenderer;

        void Awake()
        {
            lineRenderer = GetComponent<LineRenderer>();
        }

        void Start()
        {
            SetUpLaser();
        }

        void Update()
        {
            DrawCable();
        }

        void SetUpLaser()
        {
            lineRenderer.positionCount = 2;
            lineRenderer.startWidth = 0.1f;
            lineRenderer.endWidth = 0.1f;
            lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        }

        void DrawCable()
        {
            lineRenderer.SetPosition(0, cableStartPosition.position);
            lineRenderer.SetPosition(1, cableEndPosition.position);

            lineRenderer.startColor = cableColor;
            lineRenderer.endColor = cableColor;
            lineRenderer.enabled = true;
        }
    }
}
