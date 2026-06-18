using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Electric
{
    public class LightbulbBrain : MonoBehaviour
    {
        [SerializeField, Required] ElectricityProvider electricityProvider;
        [SerializeField, Required] LightbulbPresenter presenter;

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