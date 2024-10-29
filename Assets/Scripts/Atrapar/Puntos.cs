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
        if (collision.CompareTag(AzulTag))
        {
            particleEffect.SetActive(true);
            shouldMove = false;
            Destroy(circle);
            Destroy(gameObject, 2f);
        }
        if (collision.CompareTag(RojoTag))
        {
            particleEffect.SetActive(true);
            shouldMove = false;
            Destroy(circle);
            Destroy(gameObject, 2f);
        }
    }
}
