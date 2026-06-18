using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Rendering.Universal;

namespace ProjectConnections.Electric
{
    public class DoorBrain : MonoBehaviour
    {
        [SerializeField, Required] ElectricityProvider electricityProvider;
        [SerializeField, Required] DoorSoundPlayer soundPlayer;
        [SerializeField, Required] DoorPresenter presenter;

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