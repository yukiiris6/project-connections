using UnityEngine;
using Sirenix.OdinInspector;

namespace ProjectConnections.Shared
{
    public class DestroyOnAnimationEnd : MonoBehaviour
    {
        public void DestroyObject()
        {
            Destroy(gameObject.transform.parent.gameObject);
        }
    }
}
