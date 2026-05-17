using UnityEngine;

public class SocketActivation : MonoBehaviour
{
    [SerializeField] Transform connectionAnchor;

    public DoorState targetDoor;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Plug"))
        {
            PlugController plugController = other.GetComponent<PlugController>();
            if (plugController != null)
            {
                plugController.Magnetize(connectionAnchor.position, true);
            }
            targetDoor.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Plug"))
        {
            targetDoor.SetActive(false);
        }
    }
}
