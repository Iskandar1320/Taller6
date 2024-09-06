using UnityEngine;

public class BoundaryTeleport : MonoBehaviour
{
    [SerializeField] private float minX = -10f;
    [SerializeField] private float maxX = 10f;
    [SerializeField] private float minY = -5f;
    [SerializeField] private float maxY = 5f;

    // Update is called once per frame
    void Update()
    {
     
    }
    private void FixedUpdate()
    {
        Vector2 playerPosition = transform.position;

        // Verifica si el jugador se ha salido de los l�mites en el eje X
        if (playerPosition.x < minX)
        {
            playerPosition.x = maxX; // Teletransporta al jugador al extremo derecho
        }
        else if (playerPosition.x > maxX)
        {
            playerPosition.x = minX; // Teletransporta al jugador al extremo izquierdo
        }

        // Verifica si el jugador se ha salido de los l�mites en el eje Y
        if (playerPosition.y < minY)
        {
            playerPosition.y = maxY; // Teletransporta al jugador al extremo superior
        }
        else if (playerPosition.y > maxY)
        {
            playerPosition.y = minY; // Teletransporta al jugador al extremo inferior
        }

        // Actualiza la posici�n del jugador
        transform.position = playerPosition;
    }
}