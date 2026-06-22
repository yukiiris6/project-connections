using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Electric
{
    public class DoorInteractable : MonoBehaviour
    {
        [SerializeField, Required] ObjectEnergizer electricObject;
        [SerializeField, Required] DoorSoundPlayer soundPlayer;

        // public void Interact(PlayerRefs playerRefs)
        public void Interact()
        {
            if (!electricObject.IsOn) return;
            // playerRefs.transform.position = transform.position;
            // playerRefs.Animation.PlayFinishAnimation();
            soundPlayer.PlayEnteringSFX();
            // GlobalSystems.Instance.LevelManager.FinishLevel();
        }
    }
}
