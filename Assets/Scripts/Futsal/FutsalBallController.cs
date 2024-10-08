using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutsalBallController : MonoBehaviour
{
    [SerializeField] private float stopTime = 2.0f; 
    [SerializeField] private float ballPushForce = 10.0f;
    [SerializeField] private float bounceDamping = 0.8f; // Entre 0 y 1

    private Rigidbody2D rb;
    private Vector2 lastVelocity; 

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Update()
    {
        lastVelocity = rb.velocity;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Contacto con Jugador
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 direction = transform.position - collision.transform.position;
            rb.AddForce(direction.normalized * ballPushForce, ForceMode2D.Impulse);
            StartCoroutine(StopBallAfterTime());
        }
        // Contacto con Bordes
        if (collision.gameObject.CompareTag("BallBoundaries"))
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector2.Reflect(lastVelocity.normalized, collision.contacts[0].normal);

            rb.velocity = direction * Mathf.Max(speed * bounceDamping, 0f); 
        }
    }

    private IEnumerator StopBallAfterTime()
    {
        // Detiene la pelota después de un tiempo
        yield return new WaitForSeconds(stopTime);
        rb.velocity = Vector2.zero;
    }
}
