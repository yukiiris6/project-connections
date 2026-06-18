using System.Collections;
using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Electric
{
    public class DoorSoundPlayer : MonoBehaviour
    {
        [SerializeField, Required] AudioSource audioSource;
        [SerializeField, Required] AudioClip doorEnterSFX;
        [SerializeField, Required] AudioClip doorOpenSFX;
        [SerializeField, Required] AudioClip doorCloseSFX;

        Coroutine activeCoroutine;

        public void PlayOpeningSFX()
        {
            StopActiveCoroutine();
            StartCoroutine(OpeningSFXRoutine());
        }

        public void PlayClosingSFX()
        {
            StopActiveCoroutine();
            audioSource.PlayOneShot(doorCloseSFX);
        }

        public void PlayEnteringSFX()
        {
            audioSource.PlayOneShot(doorEnterSFX);
        }

        void StopActiveCoroutine()
        {
            if (activeCoroutine != null)
            {
                StopCoroutine(activeCoroutine);
                activeCoroutine = null;
            }
        }

        IEnumerator OpeningSFXRoutine()
        {
            yield return new WaitForSeconds(.25f);
            audioSource.PlayOneShot(doorOpenSFX);
        }
    }
}