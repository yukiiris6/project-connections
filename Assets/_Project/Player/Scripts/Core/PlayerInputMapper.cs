using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.InputSystem;

public class PlayerInputMapper : MonoBehaviour
{
    [SerializeField, Required] PlayerInput playerInput;

    public void ToggleActionMap()
    {
        if (playerInput.currentActionMap.name.Equals("Gameplay")) SetUIActionMap();
        else SetGameplayActionMap();
    }

    public void SetGameplayActionMap()
    {
        playerInput.SwitchCurrentActionMap("Gameplay");
    }

    public void SetUIActionMap()
    {
        playerInput.SwitchCurrentActionMap("UI");
    }
}