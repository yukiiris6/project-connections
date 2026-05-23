using DG.Tweening;
using UnityEngine;

public class LightbulbController : MonoBehaviour
{
    [SerializeField] SocketController connectedSocket;
    [SerializeField] GameObject spriteLight;

    void Start()
    {
        spriteLight.SetActive(false);
        if (connectedSocket != null) SetActive(connectedSocket.HasEnergy);
        else SetActive(true);
        Vector3 maxAngle = new(0, 0, 5f);
        transform.rotation = Quaternion.Euler(-maxAngle);
        transform.DOLocalRotate(maxAngle, 4f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
    }

    void OnEnable()
    {
        if (connectedSocket != null)
        {
            connectedSocket.OnChangeActivation += SetActive;
        }
    }

    void OnDisable()
    {
        if (connectedSocket != null)
        {
            connectedSocket.OnChangeActivation -= SetActive;
        }
    }

    public void SetActive(bool active)
    {
        spriteLight.SetActive(active);
    }
}
