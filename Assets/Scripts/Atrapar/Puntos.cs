using UnityEngine;

public class Puntos : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        if (transform.position.x > Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x)
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Colisión detectada con: " + other.name);
        if (other.CompareTag("TrianAzul"))
        {
            Debug.Log("Colisión con el jugador azul");
            Destroy(gameObject);
        }
        if (other.CompareTag("TrianRojo"))
        {
            Destroy(gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("TrianAzul"))
        {
            Debug.Log("Colisión con el jugador detectada");
            Destroy(gameObject);
        }
    }

}
