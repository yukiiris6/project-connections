using UnityEngine;

public class MovingPlatformController : MonoBehaviour
{
    [SerializeField] SocketController connectedSocket;
    [SerializeField] Vector2 moveDirection = new(1f, 0f);
    [SerializeField] float moveAmount = 4f;
    [SerializeField] float moveSpeed = 1f;

    Vector2 originalPosition;

    bool shouldMove = false;

    void Start()
    {
        originalPosition = transform.position;
        shouldMove = connectedSocket.HasEnergy;
    }

    void Update()
    {
        if (!shouldMove) return;
        float distance = Vector2.Distance(originalPosition, transform.position);
        if (distance >= moveAmount)
        {
            moveDirection = -moveDirection;
            originalPosition = transform.position;
        }
        Vector2 moveVelocity = moveDirection * moveSpeed * Time.deltaTime;
        transform.Translate(moveVelocity);
    }

    void OnEnable()
    {
        connectedSocket.OnChangeActivation += SetActive;
    }

    void OnDisable()
    {
        connectedSocket.OnChangeActivation -= SetActive;
    }

    void SetActive(bool isActive)
    {
        shouldMove = isActive;
    }
}
