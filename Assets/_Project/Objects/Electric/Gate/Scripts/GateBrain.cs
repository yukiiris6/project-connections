using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Rendering.Universal;

namespace ProjectConnections.Electric
{
    public class GateBrain : MonoBehaviour
    {
        [SerializeField, Required] ElectricityProvider electricityProvider;
        [SerializeField, Required] GateController controller;
        [SerializeField, Required] GatePresenter presenter;

        void OnEnable()
        {
            UpdateGate(electricityProvider.HasEnergy());
            electricityProvider.OnChangedState += UpdateGate;
        }

        void OnDisable()
        {
            electricityProvider.OnChangedState -= UpdateGate;
        }

        void UpdateGate(bool hasEnergy)
        {
            controller.UpdateWidth(hasEnergy);
            presenter.UpdateStatus(hasEnergy);
        }
    }
}
