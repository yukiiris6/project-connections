using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Electric
{
    public class ElectricityProvider : MonoBehaviour
    {
        [ShowInInspector, ReadOnly]
        ElectricityGenerator connectedGenerator;

        public event Action<bool> OnChangedState;

        void OnDisable()
        {
            if (connectedGenerator != null)
            {
                connectedGenerator.OnChangedState -= UpdateState;
            }
        }

        public void ConnectToGenerator(ElectricityGenerator newGenerator)
        {
            connectedGenerator = newGenerator;
            connectedGenerator.OnChangedState += UpdateState;
            OnChangedState?.Invoke(HasEnergy());
        }

        public void DisconnectFromGenerator()
        {
            connectedGenerator.OnChangedState -= UpdateState;
            connectedGenerator = null;
            OnChangedState?.Invoke(HasEnergy());
        }

        public bool HasEnergy()
        {
            if (connectedGenerator != null) return connectedGenerator.IsGenerating;
            return false;
        }

        void StartUpProvider(bool value)
        {
            OnChangedState?.Invoke(value);
            connectedGenerator.OnStartUp -= StartUpProvider;
        }

        void UpdateState(bool isGenerating)
        {
            OnChangedState?.Invoke(isGenerating);
        }
    }
}