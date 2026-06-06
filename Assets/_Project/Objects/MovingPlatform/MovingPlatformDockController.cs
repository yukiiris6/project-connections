using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MovingPlatformDockController : MonoBehaviour
{
    [SerializeField] SocketController connectedSocket;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite spriteOn;
    [SerializeField] Sprite spriteOff;
    [SerializeField] Light2D light2D;
    [SerializeField] Color offLightColor;
    [SerializeField] Color onLightColor;

    void OnEnable()
    {
        connectedSocket.OnStartUp += StartUp;
        connectedSocket.OnChangeActivation += SetActive;
    }

    void OnDisable()
    {
        connectedSocket.OnStartUp -= StartUp;
        connectedSocket.OnChangeActivation -= SetActive;
    }

    void StartUp()
    {
        SetActive(connectedSocket.HasEnergy);
    }

    void SetActive(bool isActive)
    {
        spriteRenderer.sprite = isActive ? spriteOn : spriteOff;
        light2D.color = isActive ? onLightColor : offLightColor;
    }
}
