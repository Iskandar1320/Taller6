using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipsPlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] Vector2 Speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GetBodyMovement();
        GetShootingInput();
    }

    private void GetShootingInput()
    {
        throw new NotImplementedException();
    }

    private void GetBodyMovement()
    {
        throw new NotImplementedException();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + Speed * Time.fixedDeltaTime);
    }
    public void OnShot()
    {
        Debug.Log("Mathafakas");
    }

}
