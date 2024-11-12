using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorPelota : MonoBehaviour
{
    [SerializeField] private float velocidadInicial = 20f;
    [SerializeField] private float incrementoVelocidad = 1.1f;
    [SerializeField] private float velocidadMinima = 1f;  // Umbral mínimo de velocidad

    private bool puedeSonar = true;
    private bool enZonaAnotacion = false;  // Bandera para evitar reinicio en zona de anotación
    private SpriteRenderer sprite;
    private ParticleSystem particle;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        particle = GetComponentInChildren<ParticleSystem>();
        ReinicioPelota();
    }

    private void Update()
    {
        // Solo reiniciar si la velocidad es baja y no está en zona de anotación
        if (!enZonaAnotacion && rb.velocity.magnitude < velocidadMinima)
        {
            ReinicioPelota();
        }
    }

    public void ReinicioPelota()
    {
        enZonaAnotacion = false;  // Resetear bandera al reiniciar
        sprite.enabled = true;
        transform.position = Vector2.zero;
        rb.velocity = Vector2.zero;
        velocidadInicial = 20;
        float direccionRandom = Random.Range(0, 2) * 2 - 1;

        // Aplica fuerza inicial en dirección aleatoria
        rb.AddForce(new Vector2(3 * direccionRandom, 15 * direccionRandom) * velocidadInicial, ForceMode2D.Force);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            rb.velocity *= incrementoVelocidad;

            // Limitar la velocidad máxima
            float velocidadMaxima = 30f;
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, velocidadMaxima);
            if (puedeSonar)
                StartCoroutine(WaitForSound());
        }
        if (collision.gameObject.CompareTag("BordePong") && puedeSonar)
        {
            StartCoroutine(WaitForSound());
        }
        if (collision.gameObject.CompareTag("ZonAnotaciónPong"))
        {
            enZonaAnotacion = true;  // Activar la bandera cuando llega a la zona de anotación
            StartCoroutine(ParticulasPelota());
        }
    }

    private IEnumerator ParticulasPelota()
    {
        rb.velocity = Vector2.zero;
        sprite.enabled = false;
        particle.Play();
        AudioManager.Instance.PlayOneShot(Fmod_Events.Instance.explotion);
        yield return new WaitForSeconds(1.2f);
        ReinicioPelota();
    }

    private IEnumerator WaitForSound()
    {
        puedeSonar = false;
        AudioManager.Instance.PlayOneShot(Fmod_Events.Instance.golpePelota);
        yield return new WaitForSeconds(0.4f);
        puedeSonar = true;
    }
}
