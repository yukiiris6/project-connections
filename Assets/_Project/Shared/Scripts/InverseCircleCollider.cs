using Sirenix.OdinInspector;
using UnityEngine;

public class InverseCircleCollider : MonoBehaviour
{
    [SerializeField, Required] int pointAmount = 64;
    [SerializeField, Required] float radius = 5f;
    [SerializeField, Required] EdgeCollider2D edgeCollider;
    [SerializeField, Required] Collider2D exclusiveCollider;

    LayerMask originalIncludeLayers;
    LayerMask originalExcludeLayers;

    void Awake()
    {
        originalIncludeLayers = edgeCollider.includeLayers;
        originalExcludeLayers = edgeCollider.excludeLayers;

        Vector2[] points = new Vector2[pointAmount + 1];

        for (int i = 0; i < pointAmount; i++)
        {
            float angle = 2f * Mathf.PI * i / pointAmount;
            points[i] = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        }

        points[pointAmount] = points[0];
        edgeCollider.points = points;
    }

    void Start()
    {
        Physics2D.IgnoreCollision(edgeCollider, exclusiveCollider, false);
    }
}