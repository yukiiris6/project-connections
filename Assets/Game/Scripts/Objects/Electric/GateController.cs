using UnityEngine;

public class GateController : MonoBehaviour
{
    [SerializeField] SocketController connectedSocket;
    [SerializeField] float closedWidth = 3f;
    [SerializeField] float openWidth = 1f;

    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        SetActive(connectedSocket.HasEnergy);
    }

    void OnEnable()
    {
        connectedSocket.OnChangeActivation += SetActive;
    }

    void OnDisable()
    {
        connectedSocket.OnChangeActivation -= SetActive;
    }

    void SetActive(bool isActive)
    {
        float newWidth = isActive ? openWidth : closedWidth;
        spriteRenderer.size = new(newWidth, 1f);
    }
}
