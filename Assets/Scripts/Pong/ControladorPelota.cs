using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControladorPelota : MonoBehaviour
{
    [SerializeField] private float impulso;

    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ReinicioPelota();
    }

    public void ReinicioPelota()
    {
        transform.position = Vector2.zero;
        rb.velocity = Vector2.zero;
        impulso = 25;
        float direccionRandom = Random.Range(0,2) * 2-1;
        rb.AddForce(new Vector2(3*direccionRandom, 15*direccionRandom) * impulso, ForceMode2D.Force);
    }
    public void IncrementoVelocidad_Golpe()
    {
        impulso = impulso + 5;
    } 
    public void GolpeLateral()
    {
        impulso = impulso + 3.5f;
    }
}
