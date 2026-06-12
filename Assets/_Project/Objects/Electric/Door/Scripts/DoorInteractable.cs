using UnityEngine;

namespace ProjectConnections.Electric
{
    public class DoorInteractable : MonoBehaviour
    {
        [SerializeField] ElectricityProvider electricityProvider;
        [SerializeField] DoorSoundPlayer soundPlayer;

        // public void Interact(PlayerRefs playerRefs)
        public void Interact()
        {
            if (!electricityProvider.HasEnergy()) return;
            // playerRefs.transform.position = transform.position;
            // playerRefs.Animation.PlayFinishAnimation();
            soundPlayer.PlayEnteringSFX();
            // GlobalSystems.Instance.LevelManager.FinishLevel();
        }
    }
}
