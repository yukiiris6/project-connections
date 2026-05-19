using UnityEngine;

public class SocketActivation : MonoBehaviour
{
    [SerializeField] Transform connectionAnchor;
    [SerializeField] DoorState targetDoor;
    [SerializeField] LightbulbVisuals lightbulbVisuals;
    [SerializeField] int segments = 64;

    CircleCollider2D circleCollider2D;
    LineRenderer lineRenderer;
    PlugController plugInRange;
    PlayerMagnetize playerMagnetize;

    float radius;

    void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments;
        lineRenderer.useWorldSpace = false;
        radius = circleCollider2D.radius;
        DrawCircle();
        lineRenderer.enabled = false;
    }

    void Update()
    {
        if (playerMagnetize != null && !playerMagnetize.IsAiming) lineRenderer.enabled = false;
    }

    public void SetShowCircle(bool shouldShow, PlayerMagnetize newPlayerMagnetize)
    {
        lineRenderer.enabled = shouldShow;
        playerMagnetize = newPlayerMagnetize;
    }

    public void Magnetize()
    {
        if (plugInRange)
        {
            plugInRange.ConnectToSocket(connectionAnchor.position, this);
        }
    }

    public void SetConnected(bool isConnected)
    {
        targetDoor.SetActive(isConnected);
        lightbulbVisuals.SetActive(isConnected);
    }

    public void DrawCircle()
    {
        float deltaTheta = 2f * Mathf.PI / segments;
        float theta = 0f;

        for (int i = 0; i < segments; i++)
        {
            float x = radius * Mathf.Cos(theta);
            float y = radius * Mathf.Sin(theta);

            lineRenderer.SetPosition(i, new Vector3(x, y, 0f));
            theta += deltaTheta;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Plug"))
        {
            PlugController plugController = other.GetComponent<PlugController>();
            if (plugController != null)
            {
                plugInRange = plugController;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Plug"))
        {
            PlugController plugController = other.GetComponent<PlugController>();
            if (plugController != null)
            {
                if (plugInRange == plugController) plugInRange = null;
            }
        }
    }
}
