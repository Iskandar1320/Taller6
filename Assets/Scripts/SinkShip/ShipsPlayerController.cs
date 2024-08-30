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

    
   
   

}
