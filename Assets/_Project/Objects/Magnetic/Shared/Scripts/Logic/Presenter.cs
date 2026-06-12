using Unity.Cinemachine;
using UnityEngine;

namespace ProjectConnections.Magnetism
{
    public class Presenter : MonoBehaviour
    {
        [SerializeField] CinemachineImpulseSource cinemachineImpulseSource;

        public void PlayShake()
        {
            cinemachineImpulseSource.GenerateImpulse();
        }
    }
}
