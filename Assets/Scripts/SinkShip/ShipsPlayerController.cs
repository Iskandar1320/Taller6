using System.Collections; 
using UnityEngine;

namespace SinkShip
{
    public class ShipsPlayerController : MonoBehaviour
    {
   
        [Header("Movement Settings")]
        [SerializeField] Vector2 speed;
        [SerializeField] int playerNumber;
        [SerializeField] float rotationSpeed = 90f;
        [SerializeField] SteeringWheel steeringWheel;
        [SerializeField] public int lifes = 10;
        [SerializeField] GameObject projectile;
        [SerializeField] float separationForce = 5f; // Fuerza de separación al colisionar

        [Header("Inmortality Settings")]
        [SerializeField] float invulnerabilityDuration = 2f; // Duración de la inmortalidad
        [SerializeField] float flashDuration = 0.1f; // Duración del titileo
        private bool isInvulnerable = false; // Controla si el barco es inmune

        private Rigidbody2D rb;
        private SpriteRenderer spriteRenderer;
        private float steeringInput;
    

        // Start is called before the first frame update
        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            spriteRenderer = GetComponent<SpriteRenderer>(); // Esto para manejar el titileo

            rb.angularDrag = 2f; // Ajuste de Friccion para que no gire de forma indefinida
        }


        private void FixedUpdate()
        {
            steeringInput = steeringWheel.GetClampedValue();

            float rotationAmount = steeringInput   * rotationSpeed *Time.deltaTime;
            rb.rotation += rotationAmount;

            Vector2 forwardMovement = transform.up * speed.y * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + forwardMovement);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!isInvulnerable)
            {
                lifes--;

                Debug.Log("Impacto a jugador");

                Destroy(collision.gameObject);  // Destruir el Proyectil

                if (lifes > 0)
                {
                    StartCoroutine(ActivateInvulnerability()); // Activar la inmortalidad por un tiempo

                }
                else
                {
                    Debug.Log("gano el jugador"+playerNumber);

                }
            }
            
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Vector2 separationDirection = (rb.position - (Vector2)collision.transform.position).normalized;
                rb.AddForce(separationDirection * separationForce, ForceMode2D.Impulse);

                Debug.Log("Colision entre barcos, aplicando fuerza de separacion");
            }
        }
        private IEnumerator ActivateInvulnerability()
        {
            isInvulnerable = true; // Activar el estado de inmortalidad
            float timer = 0f;

            while (timer < invulnerabilityDuration)
            {
                // Alternar la visibilidad del barco para hacer el efecto de titileo
                spriteRenderer.enabled = !spriteRenderer.enabled;
                yield return new WaitForSeconds(flashDuration); // Espera el tiempo de titileo
                timer += flashDuration;
            }

            // Asegurarse de que el barco esté visible al final
            spriteRenderer.enabled = true;

            isInvulnerable = false; // Terminar el estado de inmortalidad
        }




    }
}
