using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace ProjectConnections.Electric
{
    public class DoorBrain : MonoBehaviour
    {
        [SerializeField] ElectricityProvider electricityProvider;
        [SerializeField] DoorSoundPlayer soundPlayer;
        [SerializeField] DoorPresenter presenter;

        void OnEnable()
        {
            electricityProvider.OnChangedState += OnProviderChanged;
        }

        void OnDisable()
        {
            electricityProvider.OnChangedState -= OnProviderChanged;
        }

        public void OnProviderChanged(bool hasEnergy)
        {
            if (hasEnergy) soundPlayer.PlayOpeningSFX();
            else soundPlayer.PlayClosingSFX();
            presenter.UpdateState(hasEnergy);
        }
    }
}