using System;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(LineRenderer))]
public class SocketController : MonoBehaviour
{
    [SerializeField] PlugController connectedPlug;
    [SerializeField] Transform connectionAnchor;
    [SerializeField] AudioClip activationSound;
    [SerializeField] int segments = 64;

    AudioSource audioSource;
    CircleCollider2D circleCollider2D;
    LineRenderer lineRenderer;
    PlugController plugInRange;
    PlayerMagnetize playerMagnetize;

    bool hasStarted = false;
    float radius;
    bool hasEnergy = false;
    public bool HasEnergy => hasEnergy;
    public event Action<bool> OnChangeActivation;
    public event Action OnStartUp;
    public Vector3 ConnectionRotation { get; private set; }

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        circleCollider2D = GetComponent<CircleCollider2D>();
        lineRenderer = GetComponent<LineRenderer>();
        ConnectionRotation = connectionAnchor.rotation.eulerAngles;
        lineRenderer.positionCount = segments;
        lineRenderer.useWorldSpace = false;
        radius = circleCollider2D.radius;
        lineRenderer.enabled = false;
        DrawCircle();
    }

    void Start()
    {
        if (connectedPlug)
        {
            ChangeActivationState(true, connectedPlug);
            connectedPlug.SetStartState(this, connectionAnchor.position);
        }
        hasStarted = true;
        OnStartUp?.Invoke();
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

    public PlugController Magnetize()
    {
        if (connectedPlug)
        {
            connectedPlug.CancelMagnetism();
            return null;
        }
        if (plugInRange)
        {
            plugInRange.ConnectToSocket(connectionAnchor.position, this);
            return plugInRange;
        }
        return null;
    }

    public void ChangeActivationState(bool value, PlugController plug)
    {
        hasEnergy = value;
        if (hasStarted)
        {
            if (value)
            {
                connectedPlug = plug;
                audioSource.PlayOneShot(activationSound);
            }
            else
            {
                connectedPlug = null;
            }
            OnChangeActivation?.Invoke(value);
        }
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
