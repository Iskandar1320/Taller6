using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    public float speed = 5f;
    public GameObject particleEffect;
    public GameObject circle;
    string AzulTag = "TrianAzul";
    string RojoTag = "TrianRojo";
    private bool shouldMove = true;
    void Update()
    {
        if (shouldMove)
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (transform.position.x > Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0)).x)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameManagerAtrapar gameManagerAtrapar = FindObjectOfType<GameManagerAtrapar>(); 

        if (collision.CompareTag(AzulTag))
        {
            GetComponent<AudioSource>().Play();
            particleEffect.SetActive(true);
            shouldMove = false;
            Destroy(circle);
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 0.5f);
            gameManagerAtrapar.PuntoAzul();
        }
        if (collision.CompareTag(RojoTag))
        {
            GetComponent<AudioSource>().Play();
            particleEffect.SetActive(true);
            shouldMove = false;
            Destroy(circle);
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 0.5f);
            gameManagerAtrapar.PuntoRojo();
        }
    }
}
