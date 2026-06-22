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
        [SerializeField, Required] ObjectEnergizer objectEnergizer;
        [SerializeField, Required] GateController controller;
        [SerializeField, Required] GatePresenter presenter;

        void OnEnable()
        {
            UpdateState(objectEnergizer.IsOn);
            objectEnergizer.OnChangedState += UpdateState;
        }

        void OnDisable()
        {
            objectEnergizer.OnChangedState -= UpdateState;
        }

        void UpdateState(bool hasEnergy)
        {
            controller.UpdateWidth(hasEnergy);
            presenter.UpdateStatus(hasEnergy);
        }
    }
}
