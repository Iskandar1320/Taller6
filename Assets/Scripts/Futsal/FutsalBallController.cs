using System.Collections;
using UnityEngine;

public class FutsalBallController : MonoBehaviour
{
    [SerializeField] private float stopTime = 1.0f;
    [SerializeField] private float ballPushForce = 5.0f; // Ajustable desde el Inspector para controlar la fuerza de empuje
    [SerializeField] private float bounceDamping = 0.8f; // Entre 0 y 1
    [SerializeField] private float soundCooldown = 0.2f; // Tiempo de espera entre sonidos al colisionar con el jugador
    [SerializeField] private float minimumSpeedToStop = 0.5f; // Velocidad mínima antes de detenerse

    private Rigidbody2D rb;
    private Vector2 lastVelocity;
    private AudioSource audioSource;
    private bool canPlaySound = true; // Controla si el sonido puede reproducirse

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        lastVelocity = rb.velocity;

        // Detener la pelota si su velocidad es muy baja
        if (rb.velocity.magnitude < minimumSpeedToStop)
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Contacto con Jugador
        if (collision.gameObject.CompareTag("Players"))
        {
            Vector2 direction = transform.position - collision.transform.position;

            // Añadir una fuerza de empuje reducida que puedes ajustar desde el Inspector
            rb.AddForce(direction.normalized * ballPushForce, ForceMode2D.Impulse);

            // Reproducir sonido solo si se puede (controlado por la corrutina)
            if (canPlaySound)
            {
                audioSource.Play();
                StartCoroutine(SoundCooldownCoroutine());
            }
        }
        // Contacto con Bordes
        else if (collision.gameObject.CompareTag("BallBoundaries"))
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
            rb.velocity = direction * Mathf.Max(speed * bounceDamping, 0f);

            // Reproducir sonido de colisión en los bordes sin tiempo de espera
            audioSource.Play();
        }
    }

    private IEnumerator SoundCooldownCoroutine()
    {
        canPlaySound = false; // Bloquea la reproducción del sonido
        yield return new WaitForSeconds(soundCooldown); // Espera el tiempo de espera
        canPlaySound = true; // Permite la reproducción del sonido nuevamente
    }
}
