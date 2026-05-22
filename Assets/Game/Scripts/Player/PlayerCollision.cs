using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(CinemachineImpulseSource))]
public class PlayerCollision : MonoBehaviour
{
    [SerializeField] Transform centerAchor;
    [SerializeField] BoxCollider2D feetCollider;
    [SerializeField] LayerMask movingPlatformsLayer;
    [SerializeField] LayerMask obstacleLayer;
    [SerializeField] ParticleSystem deathParticles;

    CinemachineImpulseSource cinemachineImpulseSource;
    Transform originalParent;
    GameManager gameManager;
    LevelManager levelManager;

    void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
        originalParent = transform.parent;
    }

    void Start()
    {
        gameManager = GlobalSystems.Instance.GameManager;
        levelManager = GlobalSystems.Instance.LevelManager;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        bool isMovingPlatform = ((1 << other.gameObject.layer) & movingPlatformsLayer.value) != 0;
        bool isObstacle = LayerMaskExtensions.Contains(obstacleLayer, other.gameObject.layer);
        if (isMovingPlatform)
        {
            if (other.IsTouching(feetCollider))
            {
                transform.SetParent(other.transform);
            }
        }
        if (isObstacle)
        {
            Die();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        bool isMovingPlatform = LayerMaskExtensions.Contains(movingPlatformsLayer, gameObject.layer);
        if (isMovingPlatform)
        {
            if (transform.parent != other.transform && other.IsTouching(feetCollider))
            {
                transform.SetParent(other.transform);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (transform.parent == other.transform)
        {
            if (!other.gameObject.activeInHierarchy || !other.enabled) return;
            transform.SetParent(originalParent);
        }
    }

    void Die()
    {
        StartCoroutine(DieRoutine());
    }

    IEnumerator DieRoutine()
    {
        gameManager.PauseGame();
        yield return new WaitForSecondsRealtime(.1f);
        gameManager.ResumeGame();
        cinemachineImpulseSource.GenerateImpulse();
        Instantiate(deathParticles, centerAchor.position, Quaternion.identity);
        levelManager.Die();
        Destroy(gameObject);
    }
}
