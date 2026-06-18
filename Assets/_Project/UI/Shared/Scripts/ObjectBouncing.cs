using DG.Tweening;
using UnityEngine;
using Sirenix.OdinInspector;

public class ObjectBouncing : MonoBehaviour
{
    [SerializeField, Required] Vector3 finalRotation;
    [SerializeField, Required] float duration = 1f;

    void Start()
    {
        transform.DORotate(finalRotation, duration).
            SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
}
