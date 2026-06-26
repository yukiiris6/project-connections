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
                PlayStopEffects();
            }
        }

        public void PlayStopEffects()
        {
            cinemachineImpulseSource.GenerateImpulse();
            soundPlayer.PlayCrashSFX();
        }

        public void PlayConnectEffects()
        {
            PlayStopEffects();
            soundPlayer.PlayConnectionSFX();
        }
    }
}
