using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Electric
{
    public class ElectricityGenerator : MonoBehaviour
    {
        [field: SerializeField] public bool IsGenerating { get; private set; } = true;

        public event Action<bool> OnStartUp;
        public event Action<bool> OnChangedState;

        public void ToggleProvider(bool value)
        {
            IsGenerating = value;
            OnChangedState?.Invoke(value);
        }
    }
}