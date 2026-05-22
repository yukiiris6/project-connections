using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(AudioSource))]
public class DoorController : MonoBehaviour, IInteractable
{
    [SerializeField] SocketController connectedSocket;
    [SerializeField] AudioClip doorEnterSFX;
    [SerializeField] SpriteRenderer rend;
    [SerializeField] Animator doorAnimator;
    [SerializeField] Light2D light2D;
    [SerializeField] Color closedLightColor;
    [SerializeField] Color openLightColor;
    [SerializeField] string IsOpenBool = "IsOpen";

    AudioSource audioSource;

    public bool IsInsideDoor { get; private set; } = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        SetActive(connectedSocket.HasEnergy);
    }

    void OnEnable()
    {
        connectedSocket.OnChangeActivation += SetActive;
    }

    void OnDisable()
    {
        connectedSocket.OnChangeActivation -= SetActive;
    }

    public void SetActive(bool isActive)
    {
        SetAnimatorState(isActive);
        SetColor(isActive);
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
        if (LayerMask.LayerToName(other.gameObject.layer) == "Player" && connectedSocket.HasEnergy)
        {
            IsInsideDoor = true;
            var playerProgress = other.GetComponent<PlayerProgress>();
            playerProgress.SetInteractable(this);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Player" && connectedSocket.HasEnergy)
        {
            IsInsideDoor = false;
        }
    }

    public void Interact(GameObject player)
    {
        player.transform.position = transform.position;
        PlayerAnimator playerAnimator = player.GetComponent<PlayerAnimator>();
        audioSource.PlayOneShot(doorEnterSFX);
        playerAnimator.PlayFinishAnimation();
        GlobalSystems.Instance.LevelManager.FinishLevel();
    }
}
