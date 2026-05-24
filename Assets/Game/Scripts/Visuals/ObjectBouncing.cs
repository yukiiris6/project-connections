using DG.Tweening;
using UnityEngine;

public class ObjectBouncing : MonoBehaviour
{
    [SerializeField] Vector3 finalRotation;
    [SerializeField] float duration = 1f;

    void Start()
    {
        transform.DORotate(finalRotation, duration).
            SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
