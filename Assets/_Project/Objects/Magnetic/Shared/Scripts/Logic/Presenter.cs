using Unity.Cinemachine;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetism
{
    public class Presenter : MonoBehaviour
    {
        [SerializeField, Required] CinemachineImpulseSource cinemachineImpulseSource;

        public void PlayShake()
        {
            cinemachineImpulseSource.GenerateImpulse();
        }
    }
}
