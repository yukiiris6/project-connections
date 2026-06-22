using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Player
{
    public class LocomotionPresenter : MonoBehaviour
    {
        [SerializeField, Required] GameObject jumpDustPrefab;
        [SerializeField, Required] GameObject landDustPrefab;

        public void InstantiateJumpDust()
        {
            Instantiate(jumpDustPrefab, transform.position, Quaternion.identity);
        }

        public void InstantiateLandDust()
        {
            Instantiate(landDustPrefab, transform.position, Quaternion.identity);
        }
    }
}
