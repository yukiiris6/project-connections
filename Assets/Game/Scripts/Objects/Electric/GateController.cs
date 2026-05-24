using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

[RequireComponent(typeof(AudioSource))]
public class GateController : MonoBehaviour
{
    [SerializeField] SocketController connectedSocket;
    [SerializeField] AudioClip openSFX;
    [SerializeField] Sprite onSprite;
    [SerializeField] Sprite offSprite;
    [SerializeField] Light2D light2D;
    [SerializeField] Color offLightColor;
    [SerializeField] Color onLightColor;
    [SerializeField] float offWidth = 3f;
    [SerializeField] float onWidth = 1f;

    SpriteRenderer spriteRenderer;
    AudioSource audioSource;

    void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
        if (connectedSocket.HasEnergy)
        {
            spriteRenderer.size = new Vector2(onWidth, spriteRenderer.size.y);
            spriteRenderer.sprite = onSprite;
        }
        else
        {
            spriteRenderer.size = new Vector2(offWidth, spriteRenderer.size.y);
            spriteRenderer.sprite = offSprite;
        }
    }

    void SetActive(bool isActive)
    {
        float newWidth = isActive ? onWidth : offWidth;
        spriteRenderer.sprite = isActive ? onSprite : offSprite;
        light2D.color = isActive ? onLightColor : offLightColor;
        SetWidth(newWidth);
    }

    void SetWidth(float newWidth)
    {
        if (spriteRenderer.size.x == newWidth) return;
        float lastX = spriteRenderer.size.x;
        DOTween.To(
            () => spriteRenderer.size.x,
            x =>
            {
                float snappedX = Mathf.Round(x * 2) / 2f;
                spriteRenderer.size = new Vector2(snappedX, spriteRenderer.size.y);
                if (lastX != spriteRenderer.size.x)
                {
                    lastX = spriteRenderer.size.x;
                    audioSource.PlayOneShot(openSFX);
                }
            },
            newWidth,
            1f
        )
        .SetEase(Ease.Flash);
    }
}
