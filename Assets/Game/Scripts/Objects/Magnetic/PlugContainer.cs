using UnityEngine;

public class PlugContainer : MonoBehaviour
{
    [SerializeField] PlugController plugController;

    public PlugController PlugController => plugController;
}
