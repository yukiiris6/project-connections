using UnityEngine;

namespace ProjectConnections.Electric
{
    public class ElectricityStorage : MonoBehaviour
    {
        [field: SerializeField] public bool HasElectricity { get; private set; }

        ElectricityGenerator connectedGenerator;

        void OnDisable()
        {
            if (connectedGenerator != null)
            {
                connectedGenerator.OnChangedState -= AddElectricity;
            }
        }

        public void SetGenerator(ElectricityGenerator electricityGenerator)
        {
            if (electricityGenerator != null) electricityGenerator.OnChangedState += AddElectricity;
            else electricityGenerator.OnChangedState -= AddElectricity;
            connectedGenerator = electricityGenerator;
        }

        public void StoreElectricity(ElectricityGenerator electricityGenerator)
        {
            HasElectricity = electricityGenerator.IsGenerating;
            electricityGenerator.OnChangedState += AddElectricity;
        }

        public void AddElectricity(bool value)
        {
            if (!value) return;
            HasElectricity = value;
        }

        public void RemoveElectricity()
        {
            HasElectricity = false;
        }
    }
}