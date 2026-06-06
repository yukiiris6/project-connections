using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(AudioSource))]
public class DoorController : MonoBehaviour, IInteractable
{
    [SerializeField] SocketController connectedSocket;
    [SerializeField] AudioClip doorEnterSFX;
    [SerializeField] AudioClip doorOpenSFX;
    [SerializeField] AudioClip doorCloseSFX;
    [SerializeField] SpriteRenderer rend;
    [SerializeField] Animator doorAnimator;
    [SerializeField] Light2D light2D;
    [SerializeField] Color closedLightColor;
    [SerializeField] Color openLightColor;
    [SerializeField] string IsOpenBool = "IsOpen";

    AudioSource audioSource;
    bool hasStarted = false;
    bool hasInteracted = false;

    public bool IsInsideDoor { get; private set; } = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        connectedSocket.OnStartUp += StartUp;
        connectedSocket.OnChangeActivation += SetActive;
    }

    void OnDisable()
    {
        connectedSocket.OnStartUp -= StartUp;
        connectedSocket.OnChangeActivation -= SetActive;
    }

    void StartUp()
    {
        SetActive(connectedSocket.HasEnergy);
        hasStarted = true;
    }

    public void SetActive(bool isActive)
    {
        SetAnimatorState(isActive);
        SetColor(isActive);
        if (hasStarted)
        {
            if (isActive) StartCoroutine(PlayOpeningSound());
            else audioSource.PlayOneShot(doorCloseSFX);
        }
    }

    void SetAnimatorState(bool isActive)
    {
        doorAnimator.SetBool(IsOpenBool, isActive);
    }

    void SetColor(bool isActive)
    {
        light2D.color = isActive ? openLightColor : closedLightColor;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Player")
        {
            IsInsideDoor = true;
            var playerProgress = other.GetComponent<PlayerProgress>();
            if (playerProgress) playerProgress.SetInteractable(this);
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Player")
        {
            IsInsideDoor = true;
            var playerProgress = other.GetComponent<PlayerProgress>();
            if (playerProgress) playerProgress.SetInteractable(this);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Player")
        {
            IsInsideDoor = false;
            var playerProgress = other.GetComponent<PlayerProgress>();
            if (playerProgress) playerProgress.SetInteractable(null);
        }
    }

    public void Interact(GameObject player)
    {
        if (!connectedSocket.HasEnergy) return;
        if (hasInteracted) return;
        player.transform.position = transform.position;
        PlayerAnimator playerAnimator = player.GetComponent<PlayerAnimator>();
        audioSource.PlayOneShot(doorEnterSFX);
        playerAnimator.PlayFinishAnimation();
        GlobalSystems.Instance.LevelManager.FinishLevel();
        hasInteracted = true;
    }

    IEnumerator PlayOpeningSound()
    {
        yield return new WaitForSeconds(.3f);
        audioSource.PlayOneShot(doorOpenSFX);
    }
}
