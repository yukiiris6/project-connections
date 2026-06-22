using System;
using UnityEngine;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace ProjectConnections.Electric
{
    public class ElectricityGenerator : MonoBehaviour
    {
        [field: SerializeField] public bool IsGenerating { get; private set; } = true;

        public event Action<bool> OnChangedState;

        public void ToggleGeneration(bool value)
        {
            IsGenerating = value;
            OnChangedState?.Invoke(value);
        }
    }
}
