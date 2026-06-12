using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace ProjectConnections.Electric
{
    public class ElevatorBrain : MonoBehaviour
    {
        [SerializeField] ElectricityProvider electricityProvider;
        [SerializeField] ElevatorController controller;
        [SerializeField] ElevatorPresenter presenter;
        [SerializeField] ElevatorDockPresenter dockPresenter;

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
            controller.UpdateState(hasEnergy);
            presenter.UpdateState(hasEnergy);
            dockPresenter.UpdateState(hasEnergy);
        }
    }
}