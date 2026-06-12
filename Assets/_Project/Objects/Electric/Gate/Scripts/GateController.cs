using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace ProjectConnections.Electric
{
    public class GateController : MonoBehaviour
    {
        [SerializeField] SpriteRenderer[] spriteRenderers;
        [SerializeField] GateSoundPlayer soundPlayer;
        [SerializeField] GameObject killingTrigger;
        [SerializeField] float offWidth = 3f;
        [SerializeField] float onWidth = 1f;

        readonly List<Tween> tweens = new();

        void OnDestroy()
        {
            KillAllTweens();
        }

        public void UpdateWidth(bool hasEnergy)
        {
            if (tweens != null && tweens.Count > 0)
            {
                tweens.ForEach((tween) => tween.Kill());
            }

            float newWidth = hasEnergy ? onWidth : offWidth;
            killingTrigger.SetActive(false);
            UpdateSprites(newWidth);
        }

        void UpdateSprites(float newWidth)
        {
            Array.ForEach(spriteRenderers, (spriteRenderer) =>
            {
                Tween tween = CreateTween(spriteRenderer, newWidth);
                CompleteTween(tween);
            });
        }

        Tween CreateTween(SpriteRenderer spriteRenderer, float newWidth)
        {
            float lastSpriteX = spriteRenderer.size.x;
            Tween newTween = DOTween.To(
                () => spriteRenderer.size.x,
                newSpriteX =>
                {
                    Vector2 newSpriteSize = GetNewSnappedSpriteSize(spriteRenderer, newSpriteX);
                    PlaySoundIfDifferent(newSpriteSize.x, lastSpriteX);
                    spriteRenderer.size = newSpriteSize;
                    lastSpriteX = newSpriteSize.x;
                },
                newWidth,
                1f
            ).SetEase(Ease.Flash);
            tweens.Add(newTween);
            return newTween;
        }

        void CompleteTween(Tween tween)
        {
            tween.OnComplete(() =>
            {
                tweens.Remove(tween);
                killingTrigger.SetActive(true);
            });
        }

        Vector2 GetNewSnappedSpriteSize(SpriteRenderer spriteRenderer, float spriteRendererX)
        {
            float snapStep = 0.5f;
            float snappedX = Mathf.Round(spriteRendererX / snapStep) * snapStep;
            return new(snappedX, spriteRenderer.size.y);
        }

        void PlaySoundIfDifferent(float firstValue, float secondValue)
        {
            if (firstValue != secondValue) soundPlayer.PlayGateSFX();
        }

        void KillAllTweens()
        {
            if (tweens != null && tweens.Count > 0)
            {
                tweens.ForEach((tween) => tween.Kill());
            }
        }
    }
}
