using UnityEngine;

public class SocketActivation : MonoBehaviour
{
    [SerializeField] Transform connectionAnchor;
    [SerializeField] DoorState targetDoor;
    [SerializeField] LightbulbVisuals lightbulbVisuals;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Plug"))
        {
            PlugController plugController = other.GetComponent<PlugController>();
            if (plugController != null)
            {
                plugController.ConnectToSocket(connectionAnchor.position);
            }
            targetDoor.SetActive(true);
            lightbulbVisuals.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Plug"))
        {
            targetDoor.SetActive(false);
            lightbulbVisuals.SetActive(false);
        }
    }
}
