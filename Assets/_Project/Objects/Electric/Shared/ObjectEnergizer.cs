using System;
using UnityEngine;

namespace ProjectConnections.Electric
{
    public class ObjectEnergizer : MonoBehaviour
    {
        public bool IsOn { get; private set; }

        public event Action<bool> OnChangedState;

        public void UpdateState(bool value)
        {
            IsOn = value;
            OnChangedState?.Invoke(value);
        }
    }
}
