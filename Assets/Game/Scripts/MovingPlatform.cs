using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] float moveAmount = 4f;
    [SerializeField] float moveSpeed = 1f;

    Vector2 originalPosition;
    float moveDirection;

    void Start()
    {
        originalPosition = transform.position;
        moveDirection = moveSpeed;
    }

    void Update()
    {
        float distance = Vector2.Distance(originalPosition, transform.position);
        if (distance >= moveAmount)
        {
            moveDirection = -moveDirection;
            originalPosition = transform.position;
        }
        Vector2 moveVelocity = new(moveDirection * Time.deltaTime, 0f);
        transform.Translate(moveVelocity);
    }
}
