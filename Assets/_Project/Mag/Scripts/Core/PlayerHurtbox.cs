using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class PlayerHurtbox : MonoBehaviour
{
    [SerializeField] CinemachineImpulseSource cinemachineImpulseSource;
    [SerializeField] Transform centerAnchor;
    [SerializeField] ParticleSystem deathParticles;

    GameBrain gameManager;
    SceneLoaderBrain sceneLoader;
    bool hasDied = false;

    void Awake()
    {
        gameManager = CoreSystems.Instance.GameBrain;
        sceneLoader = CoreSystems.Instance.SceneLoader;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Die();
    }

    void Die()
    {
        if (hasDied) return;
        StartCoroutine(DieRoutine());
        hasDied = true;
    }

    IEnumerator DieRoutine()
    {
        gameManager.PauseGame();
        yield return new WaitForSecondsRealtime(.1f);
        gameManager.ResumeGame();

        PlayDeathAnimation();
        yield return new WaitForSecondsRealtime(1f);
        sceneLoader.RestartLevel();
    }

    void PlayDeathAnimation()
    {
        cinemachineImpulseSource.GenerateImpulse();
        Instantiate(deathParticles, centerAnchor.position, Quaternion.identity);
    }
}
