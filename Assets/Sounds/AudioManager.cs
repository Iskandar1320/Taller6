using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance { get; private set; }
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("hay más de un AudioManger");
        }
        instance = this;
    }
}
