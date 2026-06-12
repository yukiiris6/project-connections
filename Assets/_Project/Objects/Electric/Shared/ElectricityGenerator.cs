using System;
using UnityEngine;

namespace ProjectConnections.Electric
{
    public class ElectricityGenerator : MonoBehaviour
    {
        [SerializeField] private bool isGeneratingByDefault = true;

        public event Action<bool> OnStartUp;
        public event Action<bool> OnChangedState;
        public bool IsGenerating { get; private set; }

        void Start()
        {
            IsGenerating = isGeneratingByDefault;
            OnStartUp?.Invoke(IsGenerating);
        }

        public void ToggleProvider(bool value)
        {
            IsGenerating = value;
            OnChangedState?.Invoke(value);
        }
    }
}