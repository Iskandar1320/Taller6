using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Controlador_Barra1st : MonoBehaviour
{
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovimientoBarra1();
    }

    void MovimientoBarra1()
    {
        foreach(Touch touch in Input.touches)
        {
            Vector3 posicionTouch = Camera.main.ScreenToWorldPoint(touch.position);
            Vector2 miPosicion = rb.position;

            if(Mathf.Abs(touch.position.y - miPosicion.y) <= 2)
            {
                miPosicion.x = Mathf.Lerp(miPosicion.x, posicionTouch.x, 10);
                miPosicion.x = Mathf.Clamp(miPosicion.x, -1.82f, 1.82f);
                rb.position = miPosicion;
            }

            }

        ////                            INPUT DE MOUSE                      ////
       /* Vector2 posicionMouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 miPosicion = rb.position;

        miPosicion.x = Mathf.Lerp(miPosicion.x, posicionMouse.x, 10);
        miPosicion.x = Mathf.Clamp(miPosicion.x, -1.82f, 1.82f);
        rb.position = miPosicion;
        */
    }
    private void OnCollisionEnter2D(Collision2D golpeBola)
    {
        if(golpeBola.gameObject.GetComponent<ControladorPelota>() != null)
        {
            golpeBola.gameObject.GetComponent<ControladorPelota>().IncrementoVelocidad_Golpe();
        }
    }

}
