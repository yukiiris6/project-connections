using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Rendering.Universal;

namespace ProjectConnections.Electric
{
    public class ElevatorController : MonoBehaviour
    {
        [SerializeField, Required] Transform platformTransform;
        [SerializeField, Required] Vector2 moveDirection = new(1f, 0f);
        [SerializeField, Required] float moveDuration = 1f;

        private Tween moveTween;

        void OnDestroy()
        {
            moveTween.Kill();
        }

        public void UpdateState(bool isActive)
        {
            if (isActive) StartMovement();
            else StopMovement();
        }

        void StartMovement()
        {
            if (moveTween != null)
            {
                moveTween.Play();
                return;
            }
            Vector3 finalPosition = platformTransform.position + (Vector3)moveDirection;
            moveTween = platformTransform.DOMove(finalPosition, moveDuration)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo);
        }

        void StopMovement()
        {
            moveTween?.Pause();
        }
    }
}
