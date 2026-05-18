using UnityEngine;

public class LightbulbVisuals : MonoBehaviour
{
    [SerializeField] GameObject spriteLight;

    void Start()
    {
        spriteLight.SetActive(false);
    }

    public void SetActive(bool active)
    {
        spriteLight.SetActive(active);
    }
}
