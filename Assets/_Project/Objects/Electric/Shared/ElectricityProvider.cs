using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Electric
{
    public class ElectricityProvider : MonoBehaviour
    {
        [SerializeField, Required] ElectricityGenerator defaultElectricityGenerator;

        ElectricityGenerator connectedGenerator;
        public event Action<bool> OnChangedState;

        void Awake()
        {
            if (defaultElectricityGenerator != null)
            {
                connectedGenerator = defaultElectricityGenerator;
                defaultElectricityGenerator.OnStartUp += StartUpProvider;
            }
        }

        public void ConnectToGenerator(ElectricityGenerator newGenerator)
        {
            connectedGenerator = newGenerator;
            OnChangedState?.Invoke(HasEnergy());
        }

        public void DisconnectFromGenerator()
        {
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
            defaultElectricityGenerator.OnStartUp -= StartUpProvider;
        }
    }
}