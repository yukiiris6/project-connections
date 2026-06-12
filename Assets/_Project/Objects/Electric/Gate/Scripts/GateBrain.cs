using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace ProjectConnections.Electric
{
    public class GateBrain : MonoBehaviour
    {
        [SerializeField] ElectricityProvider electricityProvider;
        [SerializeField] GateController controller;
        [SerializeField] GatePresenter presenter;

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
            controller.UpdateWidth(hasEnergy);
            presenter.UpdateStatus(hasEnergy);
        }
    }
}
