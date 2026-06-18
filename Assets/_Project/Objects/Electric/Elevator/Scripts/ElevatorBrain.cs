using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Rendering.Universal;

namespace ProjectConnections.Electric
{
    public class ElevatorBrain : MonoBehaviour
    {
        [SerializeField, Required] ElectricityProvider electricityProvider;
        [SerializeField, Required] ElevatorController controller;
        [SerializeField, Required] ElevatorPresenter presenter;
        [SerializeField, Required] ElevatorDockPresenter dockPresenter;

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