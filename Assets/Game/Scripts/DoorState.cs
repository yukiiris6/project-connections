using UnityEngine;

public class DoorState : MonoBehaviour
{
    [SerializeField] SpriteRenderer rend;
    [SerializeField] Color activeDoorColor;
    [SerializeField] Color inactiveDoorColor;

    public bool isDoorActive = false;
    public bool isInsideDoor = false;

    void Start()
    {
        rend.color = inactiveDoorColor;
    }

    public void SetActive(bool active)
    {
        isDoorActive = active;
        rend.color = active ? activeDoorColor : inactiveDoorColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(LayerMask.LayerToName(other.gameObject.layer) == "Player" && isDoorActive)
        {
            isInsideDoor = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(LayerMask.LayerToName(other.gameObject.layer) == "Player" && isDoorActive)
        {
            isInsideDoor = false;
        }
    }
}
