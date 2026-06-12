using System;
using UnityEngine;

namespace ProjectConnections.Electric
{
    public class ElectricityConsumer : MonoBehaviour
    {
        [field: SerializeField] public ElectricityProvider electricityProvider;

        public bool HasElectricity { get; private set; }
        public event Action<bool> OnChangedState;

        void OnEnable()
        {
            electricityProvider.OnChangedState += ToggleElectricity;
        }

        void OnDisable()
        {
            electricityProvider.OnChangedState -= ToggleElectricity;
        }

        void ToggleElectricity(bool value)
        {
            HasElectricity = value;
            OnChangedState?.Invoke(value);
        }
    }
}
