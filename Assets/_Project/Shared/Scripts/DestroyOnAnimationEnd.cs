using UnityEngine;
using Sirenix.OdinInspector;

public class DestroyOnAnimationEnd : MonoBehaviour
{
    public void DestroyObject()
    {
        Destroy(gameObject.transform.parent.gameObject);
    }
}
