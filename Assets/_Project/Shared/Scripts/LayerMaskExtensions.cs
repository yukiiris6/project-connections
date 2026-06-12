using UnityEngine;

public static class LayerMaskExtensions
{
    public static bool Contains(LayerMask layerGroup, int layer)
    {
        return (layerGroup.value & (1 << layer)) > 0;
    }
}