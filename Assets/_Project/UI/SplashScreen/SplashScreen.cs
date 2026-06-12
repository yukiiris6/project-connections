using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SplashScreen : MonoBehaviour
{
    [SerializeField] AudioClip jingle;
    [SerializeField] CanvasGroup logoCanvasGroup;
    [SerializeField] ParticleSystem snowParticles;
    [SerializeField] Image blackImage;
    [SerializeField] Transform splashScreenParent;

    AudioSource audioSource;
    SceneLoaderBrain sceneLoader;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        sceneLoader = CoreSystems.Instance.SceneLoader;
        StartCoroutine(SplashScreenRoutine());
    }

    IEnumerator SplashScreenRoutine()
    {
        yield return null;
        Instantiate(snowParticles, splashScreenParent);
        UISystems.Instance.OverlayCanvas.CursorController.HideCursor();
        yield return new WaitForSeconds(1f);
        logoCanvasGroup.alpha = 0;
        logoCanvasGroup.DOFade(1f, 2f).SetEase(Ease.InSine);
        yield return new WaitForSeconds(2f);
        audioSource.PlayOneShot(jingle);
        yield return new WaitForSeconds(2f);
        logoCanvasGroup.DOFade(0f, 2f).SetEase(Ease.OutSine);
        blackImage.DOFade(0f, 2f);
        yield return new WaitForSeconds(2f);
        sceneLoader.GoToTitleScreen();
    }
}
