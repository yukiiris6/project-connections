using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic
{
    public class PluggableCables : MonoBehaviour
    {
        [SerializeField, Required] Transform cableStartPosition;
        [SerializeField, Required] Transform cableEndPosition;
        [SerializeField, Required] Color cableColor;

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
