using System.Collections;
using UnityEngine;

namespace SinkShip
{
    public class BoundaryTeleport : MonoBehaviour
    {
        [SerializeField] private float minX = -16f;
        [SerializeField] private float maxX = 16f;
        [SerializeField] private float minY = -9f;
        [SerializeField] private float maxY = 9f;
        [SerializeField] private float teleportDelay = 1.5f; // Delay de 1 segundo entre teletransportes
        [SerializeField] private RectTransform blueShotingButton;
        [SerializeField] private RectTransform blueSteeringWheel;
        [SerializeField] private RectTransform redShotingButton;
        [SerializeField] private RectTransform redSteeringWheel;

        private bool canTeleport = true;

        // Update is called once per frame
        void Update()
        {

        }
        private void Start()
        {
            resolution();
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
        private void resolution()
        {
            float aspectRatio = (float)Screen.height / Screen.width;
            print(aspectRatio);

            // Definir una tolerancia mayor para la comparación
            float iphoneTolerance = 0.1f;
            float ipadTolerance = 0.2f;

            // Si el aspecto es cercano a 16:9 (~1.78)
            if (Mathf.Abs(aspectRatio - 1.78f) <= iphoneTolerance)
            {
                Debug.Log("iphone");
                // Ajustes de rango de movimiento para 16:9
                minX = -16f;
                maxX = 16f;
                minY = -9f;
                maxY = 9f;

                blueShotingButton.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                RectTransform blueShotingRect = blueShotingButton.GetComponent<RectTransform>();
                Debug.Log("Blue Shooting Position: " + blueShotingRect.anchoredPosition);
                blueShotingRect.sizeDelta = new Vector2(70.90002f, 232.8f); // Cambia el tamaño al que necesites
                //blueShotingRect.anchoredPosition = new Vector2(-70f, 220f); // Cambia la posición según sea necesario

                blueSteeringWheel.transform.localScale = new Vector3(110f, 110f, 1.0f);
                RectTransform blueSteeringRect = blueSteeringWheel.GetComponent<RectTransform>();
                Debug.Log("Blue Steering Wheel Position: " + blueSteeringRect.anchoredPosition);
                blueSteeringRect.sizeDelta = new Vector2(2.5f, 2.5f); // Cambia el tamaño al que necesites
                //blueSteeringRect.anchoredPosition = new Vector2(150f, 220f); // Cambia la posición según sea necesario

                redShotingButton.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                RectTransform redShotingRect = redShotingButton.GetComponent<RectTransform>();
                redShotingRect.sizeDelta = new Vector2(70.90002f, 232.8f); // Cambia el tamaño al que necesites
                //redShotingRect.anchoredPosition = new Vector2(70f, -220f); // Cambia la posición según sea necesario

                redSteeringWheel.transform.localScale = new Vector3(110f, 110f, 1.0f);
                RectTransform redSteeringRect = redSteeringWheel.GetComponent<RectTransform>();
                redSteeringRect.sizeDelta = new Vector2(2.5f, 2.5f); // Cambia el tamaño al que necesites
                //redSteeringRect.anchoredPosition = new Vector2(-150f, -220f); // Cambia la posición según sea necesario

           

            }
            // Si el aspecto es cercano a 4:3 (~1.33333 para iPad)
            else if (Mathf.Abs(aspectRatio - 1.33333f) <= ipadTolerance)
            {
                Debug.Log("ipad");
                // Ajustes de rango de movimiento para 4:3
                minX = -16f;
                maxX = 16f;
                minY = -10f;
                maxY = 10f;

                blueShotingButton.transform.localScale = new Vector3(0.8f, 0.8f, 1.0f);
                RectTransform blueShotingRect = blueShotingButton.GetComponent<RectTransform>();
                blueShotingRect.sizeDelta = new Vector2(70.90002f, 232.8f); // Cambia el tamaño al que necesites
                //blueShotingRect.anchoredPosition = new Vector2(-300f, 95f); // Cambia la posición según sea necesario

                blueSteeringWheel.transform.localScale = new Vector3(100f, 100f, 1.0f);
                RectTransform blueSteeringRect = blueSteeringWheel.GetComponent<RectTransform>();
                blueSteeringRect.sizeDelta = new Vector2(2.5f, 2.5f); // Cambia el tamaño al que necesites
                //blueSteeringRect.anchoredPosition = new Vector2(300f, 95f); // Cambia la posición según sea necesario

                redShotingButton.transform.localScale = new Vector3(0.8f, 0.8f, 1.0f);
                RectTransform redShotingRect = redShotingButton.GetComponent<RectTransform>();
                redShotingRect.sizeDelta = new Vector2(70.90002f, 232.8f); // Cambia el tamaño al que necesites
                //redShotingRect.anchoredPosition = new Vector2(300f, -95f); // Cambia la posición según sea necesario

                redSteeringWheel.transform.localScale = new Vector3(100f, 100f, 1.0f);
                RectTransform redSteeringRect = redSteeringWheel.GetComponent<RectTransform>();
                redSteeringRect.sizeDelta = new Vector2(2.5f, 2.5f); // Cambia el tamaño al que necesites
                //redSteeringRect.anchoredPosition = new Vector2(-300f, -95f); // Cambia la posición según sea necesario

                
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