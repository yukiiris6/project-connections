using System.Collections;
using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using ProjectConnections.Core;
using ProjectConnections.UI.Overlay;

namespace ProjectConnections.SceneUI
{
    public class SplashScreen : MonoBehaviour
    {
        [Header("References")]
        [SerializeField, Required] AudioClip jingle;
        [SerializeField, Required] CanvasGroup logoCanvasGroup;
        [SerializeField, Required] ParticleSystem snowParticles;
        [SerializeField, Required] Transform splashScreenParent;
        [SerializeField, Required] AudioSource audioSource;

        [Header("Values")]
        [SerializeField] float snowFadeTime = 1f;
        [SerializeField] float jingleTime = 2f;
        [SerializeField] float fadeTime = 2f;

        SceneLoaderBrain sceneLoader;

        void Start()
        {
            sceneLoader = CoreSystems.Instance.SceneLoaderBrain;
            StartCoroutine(SplashScreenRoutine());
        }

        IEnumerator SplashScreenRoutine()
        {
            yield return null;
            OverlaySystems.Instance.CursorPresenter.HideCursor();
            logoCanvasGroup.alpha = 0;

            Instantiate(snowParticles, splashScreenParent);
            yield return new WaitForSeconds(snowFadeTime);

            yield return logoCanvasGroup
                .DOFade(1f, fadeTime)
                .SetEase(Ease.InSine)
                .WaitForCompletion();

            audioSource.PlayOneShot(jingle);
            yield return new WaitForSeconds(jingleTime);

            yield return logoCanvasGroup
                .DOFade(0f, fadeTime)
                .SetEase(Ease.OutSine)
                .WaitForCompletion();

            sceneLoader.GoToTitleScreen();
        }
    }
}
