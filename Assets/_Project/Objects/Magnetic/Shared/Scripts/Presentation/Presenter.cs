using Unity.Cinemachine;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Magnetic
{
    public class Presenter : MonoBehaviour
    {
        [SerializeField, Required] CinemachineImpulseSource cinemachineImpulseSource;
        [SerializeField, Required] SoundPlayer soundPlayer;
        [SerializeField, Required] float minDistanceToCrash = 1f;

        public void PlayStopByDistance(float distanceTravelled)
        {
            if (distanceTravelled >= minDistanceToCrash)
            {
                PlayCrashEffects();
            }
        }

        public void PlayConnectEffects()
        {
            PlayCrashEffects();
            soundPlayer.PlayConnectionSFX();
        }

        public void PlayCrashEffects()
        {
            cinemachineImpulseSource.GenerateImpulse();
            soundPlayer.PlayCrashSFX();
        }
    }
}
