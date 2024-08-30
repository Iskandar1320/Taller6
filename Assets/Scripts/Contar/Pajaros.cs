using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pajaros : MonoBehaviour
{   
    void Update()
    {
        if(transform.position.y < -6f)
        {
            Destroy(gameObject);
        }
    }
}
