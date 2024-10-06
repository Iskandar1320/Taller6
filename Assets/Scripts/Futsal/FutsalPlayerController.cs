using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutsalPlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] Joystick joystickDigital;
    [SerializeField] private float moveSpeed = 5f;  // Velocidad de movimiento
    [SerializeField] GameObject ball;

    private Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Lee la dirección del joystick
        movement = joystickDigital.Direction;
    }

    private void FixedUpdate()
    {
        // Aplicar el movimiento al Rigidbody2D
        MovePlayer();
    }

    private void MovePlayer()
    {
        Vector2 moveVelocity = movement * moveSpeed;

        rb.AddForce(moveVelocity);
    }
}
