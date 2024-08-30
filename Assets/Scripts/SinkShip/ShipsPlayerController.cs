using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipsPlayerController : MonoBehaviour
{
   
    [Header("Movement Settings")]
    [SerializeField] Vector2 speed;
    [SerializeField] int playerNumber;
    [SerializeField] float rotationSpeed = 90f;
    [SerializeField] SteeringWheel steeringWheel;
    [SerializeField] public int lifes = 10;
    [SerializeField] GameObject projectile;

    private Rigidbody2D rb;
    private float steeringInput;
    

    // Start is called before the first frame update
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        steeringInput = steeringWheel.GetClampedValue();

        float rotationAmount = steeringInput   * rotationSpeed *Time.deltaTime;
        rb.rotation += rotationAmount;

        Vector2 forwardMovement = transform.up * speed.y * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + forwardMovement);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        lifes--;

        Debug.Log("Impacto a jugador");

        Destroy(collision.gameObject);

        if ( lifes <= 0 )
        {
            Debug.Log("gano el jugador x");
        }
    }




}
