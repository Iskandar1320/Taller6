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
        if (other.CompareTag("TrianAzul"))
        {
            Destroy(this.gameObject);
        }
        if (other.CompareTag("TrianRojo"))
        {
            Destroy(gameObject);
        }
    }

}
