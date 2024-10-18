using UnityEngine;

public class PersonajeAzul : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 direction = Vector2.left;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 touchPosition = Input.mousePosition;
            if (touchPosition.y > Screen.height / 2)
            {
                direction = -direction;
            }
        }
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
