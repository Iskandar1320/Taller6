using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Controlador_Barra1st : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] Rigidbody2D rbBarra1stPl;
    [SerializeField] Rigidbody2D rbBarrra2ndPl;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        foreach (Touch touch in Input.touches)
        {
            Vector3 posicionTouch = Camera.main.ScreenToWorldPoint(touch.position);

            if (touch.position.y > Screen.height / 2) //MITAD SUPERIOR OJO!!
            {
                MovimientoBarra(rbBarra1stPl, posicionTouch.x); //BARRA 1ST PLAYER (LA DE ARRIBA)
            }
            if (touch.position.y <= Screen.height / 2) //MITAD INFERIOR
            {
                MovimientoBarra(rbBarrra2ndPl, posicionTouch.x);
            }
        }
    }

    void MovimientoBarra(Rigidbody2D rb , float posicionX)
    {
        Vector2 miPosicion = rb.position;
        miPosicion.x = Mathf.Lerp(miPosicion.x, posicionX, 10 * Time.deltaTime);
        miPosicion.x = Mathf.Clamp(miPosicion.x, -1.82f, 1.82f);  // Limitar la posición en el eje X
        rb.position = miPosicion;
        /*
        foreach (Touch touch in Input.touches)
        {
            //if (touch.phase == TouchPhase.Moved)
            
                Vector2 miPosicion = rb.position;
                miPosicion.x = Mathf.Lerp(miPosicion.x, posicionX, 10 * Time.deltaTime);
                miPosicion.x = Mathf.Clamp(miPosicion.x, -1.82f, 1.82f);  // Limitar la posición en el eje X
                rb.position = miPosicion;

            /* Convertir la posición del toque de pantalla a coordenadas del mundo
            Vector3 posicionTouch = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, Camera.main.nearClipPlane));
           Vector2 miPosicion = rb.position;


        }
        */
        ////                            INPUT DE MOUSE                      ////
       /* Vector2 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 miPosicion = rb.position;

        miPosicion.x = Mathf.Lerp(miPosicion.x, posicionMouse.x, 10);
        miPosicion.x = Mathf.Clamp(miPosicion.x, -1.82f, 1.82f);
        rb.position = miPosicion;
        */
    }

    /*
     private void OnCollisionEnter2D(Collision2D golpeBola)
    {
        if(golpeBola.gameObject.GetComponent<ControladorPelota>() != null)
        {
            golpeBola.gameObject.GetComponent<ControladorPelota>().IncrementoVelocidad_Golpe();
        }
    }*/

}
