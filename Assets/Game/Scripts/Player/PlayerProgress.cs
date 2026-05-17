using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerProgress : MonoBehaviour
{
    DoorState doorState;
    void Start()
    {
        doorState = FindFirstObjectByType<DoorState>();
    }
    void OnActivateDoor(InputValue value)
    {
        if(value.isPressed && doorState.isDoorActive && doorState.isInsideDoor)
        {
            print("parabens voce passou");
        }
    }
}
