using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Electric
{
    public class ElectricityReceiver : MonoBehaviour
    {
        [SerializeField, Required] ObjectEnergizer[] connectedObjects;
        public ElectricityProvider Provider { get; private set; }

        public void SetProvider(ElectricityProvider newProvider)
        {
            Provider = newProvider;
            UpdateObjects();
        }

        public void UpdateObjects()
        {
            foreach (var objectEnergizer in connectedObjects)
            {
                bool isProviding = false;

                if (Provider != null)
                {
                    isProviding = Provider.IsProviding();
                }

                objectEnergizer.UpdateState(isProviding);
            }
        }
    }
}
