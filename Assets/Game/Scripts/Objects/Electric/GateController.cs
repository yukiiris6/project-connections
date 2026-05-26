using System;
using System.Collections.Generic;
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
    [SerializeField] GameObject killingTrigger;

    SpriteRenderer[] spriteRenderers;
    AudioSource audioSource;

    readonly List<Tween> tweens = new();

    void Awake()
    {
        spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
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
            Array.ForEach(spriteRenderers, (spriteRenderer) =>
            {
                spriteRenderer.size = new Vector2(onWidth, spriteRenderer.size.y);
                spriteRenderer.sprite = onSprite;
            });
        }
        else
        {
            Array.ForEach(spriteRenderers, (spriteRenderer) =>
            {
                spriteRenderer.size = new Vector2(offWidth, spriteRenderer.size.y);
                spriteRenderer.sprite = offSprite;
            });
        }
    }

    void SetActive(bool isActive)
    {
        float newWidth = isActive ? onWidth : offWidth;
        Array.ForEach(spriteRenderers, (spriteRenderer) =>
        {
            spriteRenderer.sprite = isActive ? onSprite : offSprite;
        });
        light2D.color = isActive ? onLightColor : offLightColor;
        SetWidth(newWidth);
    }

    void SetWidth(float newWidth)
    {
        if (tweens != null && tweens.Count > 0) tweens.ForEach((tween) => tween.Kill());

        Array.ForEach(spriteRenderers, (spriteRenderer) =>
        {
            Tween myTween;
            float lastX = spriteRenderer.size.x;
            myTween = DOTween.To(
                () => spriteRenderer.size.x,
                x =>
                {
                    killingTrigger.SetActive(false);
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
            tweens.Add(myTween);
            myTween.OnComplete(() =>
            {
                tweens.Remove(myTween);
                killingTrigger.SetActive(true);
            });
        });
    }
}
