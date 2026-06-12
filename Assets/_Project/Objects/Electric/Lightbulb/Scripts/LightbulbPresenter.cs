using DG.Tweening;
using UnityEngine;

namespace ProjectConnections.Electric
{
    public class LightbulbPresenter : MonoBehaviour
    {
        [SerializeField] GameObject spriteLight;

        void Start()
        {
            Vector3 maxAngle = new(0, 0, 5f);
            transform.rotation = Quaternion.Euler(-maxAngle);
            transform.DOLocalRotate(maxAngle, 4f).SetEase(Ease.InOutQuad).SetLoops(-1, LoopType.Yoyo);
            spriteLight.SetActive(false);
        }

        public void UpdateState(bool active)
        {
            spriteLight.SetActive(active);
        }
    }
}