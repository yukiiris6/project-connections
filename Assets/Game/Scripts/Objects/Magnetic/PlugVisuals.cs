using UnityEngine;
using Unity.Cinemachine
;
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(CinemachineImpulseSource))]
public class PlugVisuals : MonoBehaviour
{
    [SerializeField] Transform cableStartPosition;
    [SerializeField] Transform cableEndPosition;
    [SerializeField] Color cableColor;

    LineRenderer lineRenderer;
    CinemachineImpulseSource cinemachineImpulseSource;

    void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
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

    public void PlayScreenShake()
    {
        cinemachineImpulseSource.GenerateImpulse();
    }
}
