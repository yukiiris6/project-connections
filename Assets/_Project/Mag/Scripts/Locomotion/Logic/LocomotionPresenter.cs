using UnityEngine;

public class LocomotionPresenter : MonoBehaviour
{
    [SerializeField] GameObject jumpDustPrefab;
    [SerializeField] GameObject landDustPrefab;

    public void InstantiateJumpDust()
    {
        Instantiate(jumpDustPrefab, transform.position, Quaternion.identity);
    }

    public void InstantiateLandDust()
    {
        Instantiate(landDustPrefab, transform.position, Quaternion.identity);
    }
}