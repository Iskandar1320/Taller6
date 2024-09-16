using System.Collections;
using UnityEngine;

namespace SinkShip
{
    public class BoundaryTeleport : MonoBehaviour
    {
        [SerializeField] private float minX = -10f;
        [SerializeField] private float maxX = 10f;
        [SerializeField] private float minY = -5f;
        [SerializeField] private float maxY = 5f;
        [SerializeField] private float teleportDelay = 1f; // Delay de 1 segundo entre teletransportes

        private bool canTeleport = true;

        // Update is called once per frame
        void Update()
        {

        }

        private void FixedUpdate()
        {
            if (canTeleport)
            {
                Vector2 playerPosition = transform.position;
                bool hasTeleported = false;

                // Verifica si el jugador se ha salido de los límites en el eje X
                if (playerPosition.x < minX)
                {
                    playerPosition.x = maxX; // Teletransporta al jugador al extremo derecho
                    hasTeleported = true;
                }
                else if (playerPosition.x > maxX)
                {
                    playerPosition.x = minX; // Teletransporta al jugador al extremo izquierdo
                    hasTeleported = true;
                }

                // Verifica si el jugador se ha salido de los límites en el eje Y
                if (playerPosition.y < minY)
                {
                    playerPosition.y = maxY; // Teletransporta al jugador al extremo superior
                    hasTeleported = true;
                }
                else if (playerPosition.y > maxY)
                {
                    playerPosition.y = minY; // Teletransporta al jugador al extremo inferior
                    hasTeleported = true;
                }

                // Actualiza la posición del jugador si ha sido teletransportado
                if (hasTeleported)
                {
                    transform.position = playerPosition;
                    StartCoroutine(TeleportCooldown()); // Inicia el cooldown después del teletransporte
                }
            }
        }

        private IEnumerator TeleportCooldown()
        {
            canTeleport = false; // Desactivar el teletransporte temporalmente
            yield return new WaitForSeconds(teleportDelay); // Esperar el tiempo de cooldown
            canTeleport = true; // Reactivar el teletransporte
        }
    }
}