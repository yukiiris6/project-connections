using UnityEngine;

public class LightbulbController : MonoBehaviour
{
    [SerializeField] SocketController connectedSocket;
    [SerializeField] GameObject spriteLight;

    void Start()
    {
        spriteLight.SetActive(false);
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

    public void SetActive(bool active)
    {
        spriteLight.SetActive(active);
    }
}
