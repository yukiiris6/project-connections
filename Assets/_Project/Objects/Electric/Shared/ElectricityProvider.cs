using System;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Electric
{
    public class ElectricityProvider : MonoBehaviour
    {
        [SerializeField, Required] ElectricityStorage electricityStorage;

        public bool IsProviding()
        {
            return electricityStorage.HasElectricity;
        }
    }
}
