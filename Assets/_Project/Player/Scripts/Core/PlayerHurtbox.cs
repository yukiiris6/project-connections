using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using Sirenix.OdinInspector;
using ProjectConnections.Core;
using ProjectConnections.UIShared;

namespace ProjectConnections.Player
{
    public class PlayerHurtbox : MonoBehaviour
    {
        [SerializeField, Required] CinemachineImpulseSource cinemachineImpulseSource;
        [SerializeField, Required] Transform centerAnchor;
        [SerializeField, Required] ParticleSystem deathParticles;
        [SerializeField, Required] GameObject modelObject;
        [SerializeField, Required] PlayerInputMapper inputMapper;
        [SerializeField, Required] PlayerSoundPlayer soundPlayer;

        GameStateSetterBrain gameStateSetter;
        SceneLoaderBrain sceneLoader;
        MusicPlayer musicPlayer;
        bool hasDied = false;

        void Start()
        {
            gameStateSetter = CoreSystems.Instance.GameStateSetter;
            sceneLoader = CoreSystems.Instance.SceneLoaderBrain;
            musicPlayer = CoreSystems.Instance.MusicPlayer;
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
            inputMapper.ToggleInput(false);
            modelObject.SetActive(false);
            gameStateSetter.ResumeGame();
            soundPlayer.PlayDeathSFX();
            musicPlayer.PauseMusic();

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
