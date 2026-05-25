using UnityEngine;
using UnityEngine.InputSystem;

public class MouseFollower : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private Transform playerTransform; // Arraste a Mag aqui no Inspector!

    [Header("Configurações de Limite")]
    [Tooltip("Distância máxima em metros que a mira pode se afastar do jogador.")]
    [SerializeField] private float maxDistance = 3.5f;

    private Camera mainCamera;

    void Awake()
    {
        mainCamera = Camera.main;

        // Caso esqueça de arrastar no Inspector, tenta buscar automaticamente pela Tag
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null) playerTransform = player.transform;
        }
    }

    void Update()
    {
        // 1. Pega a posição real do mouse convertida para o mundo 2D
        Vector3 mouseScreenPos = Mouse.current.position.ReadValue();
        Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
        mouseWorldPos.z = 0f;

        if (playerTransform != null)
        {
            // 2. Calcula o vetor de direção e a distância atual entre o Player e o Mouse
            Vector3 direction = mouseWorldPos - playerTransform.position;
            float currentDistance = direction.magnitude;

            // 3. Se o mouse tentar ir além do limite, limita o objeto na "borda" do raio máximo
            if (currentDistance > maxDistance)
            {
                transform.position = playerTransform.position + direction.normalized * maxDistance;
            }
            else
            {
                // Se estiver dentro do limite, segue o mouse normalmente
                transform.position = mouseWorldPos;
            }
        }
        else
        {
            // Caso não encontre o player por algum motivo, segue o mouse puro para não travar o jogo
            transform.position = mouseWorldPos;
        }
    }
}