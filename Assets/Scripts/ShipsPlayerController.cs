using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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
