using DG.Tweening;
using UnityEngine;

namespace ProjectConnections.Electric
{
    public class LightbulbBrain : MonoBehaviour
    {
        [SerializeField] ElectricityProvider electricityProvider;
        [SerializeField] LightbulbPresenter presenter;

        void OnEnable()
        {
            electricityProvider.OnChangedState += OnProviderChanged;
        }

        void OnDisable()
        {
            electricityProvider.OnChangedState -= OnProviderChanged;
        }

        void OnProviderChanged(bool hasEnergy)
        {
            presenter.UpdateState(hasEnergy);
        }
    }
}