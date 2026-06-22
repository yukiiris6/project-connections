using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.Core;

namespace ProjectConnections.Player
{
    public class PlayerHurtbox : MonoBehaviour
    {
        [SerializeField, Required] CinemachineImpulseSource cinemachineImpulseSource;
        [SerializeField, Required] Transform centerAnchor;
        [SerializeField, Required] ParticleSystem deathParticles;

        GameStateSetterBrain gameStateSetter;
        SceneLoaderBrain sceneLoader;
        bool hasDied = false;

        void Start()
        {
            gameStateSetter = CoreSystems.Instance.GameStateSetter;
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
            gameStateSetter.PauseGame();
            yield return new WaitForSecondsRealtime(.1f);
            gameStateSetter.ResumeGame();

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
}
