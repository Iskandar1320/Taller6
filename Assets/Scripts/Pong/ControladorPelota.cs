using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorPelota : MonoBehaviour
{
    [SerializeField] private float velocidadInicial = 20f;
    [SerializeField] private float incrementoVelocidad = 1.1f;

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

    public void ReinicioPelota()
    {
        sprite.enabled = true;
        transform.position = Vector2.zero;
        rb.velocity = Vector2.zero;
        velocidadInicial = 20;
        float direccionRandom = Random.Range(0, 2) * 2 - 1;
        rb.AddForce(new Vector2(3 * direccionRandom, 15 * direccionRandom) * velocidadInicial, ForceMode2D.Force);
    }
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Paddle"))
        {
            rb.velocity *= incrementoVelocidad;

            // Limitar la velocidad máxima
            float velocidadMaxima = 30f; // Por ejemplo
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, velocidadMaxima);
        }
        if (collision.gameObject.CompareTag("ZonAnotaciónPong"))
        {
            StartCoroutine(ParticulasPelota());
        }
    }
      /*/ public void IncrementoVelocidad_Golpe()
      {
          velocidadInicial+= 12;
      } 
      public void GolpeLateral()
      {
          velocidadInicial += 6.5f;
      }*/

    private IEnumerator ParticulasPelota()
    {
        rb.velocity = Vector2.zero;
        sprite.enabled = false;
        particle.Play();
        



        yield return new WaitForSeconds(1.2f);
        ReinicioPelota();
    }

}
