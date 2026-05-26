using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class PlayerHurtbox : MonoBehaviour
{
    [SerializeField] Transform centerAchor;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] LayerMask obstacleLayer;

    CinemachineImpulseSource cinemachineImpulseSource;
    GameManager gameManager;
    LevelManager levelManager;

    bool hasDied = false;

    void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    void Start()
    {
        gameManager = GlobalSystems.Instance.GameManager;
        levelManager = GlobalSystems.Instance.LevelManager;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        bool isObstacle = LayerMaskExtensions.Contains(obstacleLayer, other.gameObject.layer);
        if (isObstacle)
        {
            Die();
        }
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
        cinemachineImpulseSource.GenerateImpulse();
        Instantiate(deathParticles, centerAchor.position, Quaternion.identity);
        levelManager.Die();
        Destroy(transform.parent.gameObject);
    }
}
