using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ActivarVideo : MonoBehaviour
{
    [SerializeField] VideoPlayer player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Prueba()
    {
        player.Play();

    }
}
