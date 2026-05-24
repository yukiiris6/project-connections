using DG.Tweening;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class MovingPlatformController : MonoBehaviour
{
    [SerializeField] SocketController connectedSocket;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite spriteOn;
    [SerializeField] Sprite spriteOff;
    [SerializeField] Vector2 moveDirection = new(1f, 0f);
    [SerializeField] Light2D light2D;
    [SerializeField] Color offLightColor;
    [SerializeField] Color onLightColor;
    [SerializeField] float moveAmount = 4f;
    [SerializeField] float moveSpeed = 1f;

    private Tween moveTween;

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
    }

    void SetActive(bool isActive)
    {
        spriteRenderer.sprite = isActive ? spriteOn : spriteOff;
        light2D.color = isActive ? onLightColor : offLightColor;
        if (isActive)
        {
            StartMovement();
        }
        else
        {
            StopMovement();
        }
    }

    void StartMovement()
    {
        if (moveTween != null)
        {
            moveTween.Play();
            return;
        }
        Vector3 finalPosition = transform.position + (Vector3)(moveDirection * moveAmount);
        float duration = moveAmount / moveSpeed;
        moveTween = transform.DOMove(finalPosition, duration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    void StopMovement()
    {
        moveTween?.Pause();
    }

    void OnDestroy()
    {
        moveTween.Kill();
    }
}
