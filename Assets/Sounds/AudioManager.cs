using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    [field: Header("SFX")]
    [field: SerializeField] public EventReference disparo { get; private set; }
    [field: SerializeField] public EventReference pajaro { get; private set; }
    [field: SerializeField] public EventReference golpeSumo { get; private set; }
    [field: SerializeField] public EventReference golpePelota { get; private set; }
    [field: SerializeField] public EventReference cartas { get; private set; }
    [field: SerializeField] public EventReference gol { get; private set; }
    [field: SerializeField] public EventReference winChant { get; private set; }
    [field: SerializeField] public EventReference destrucciónObjeto { get; private set; }
    [field: SerializeField] public EventReference destrucciónBarcos { get; private set; }

    [field: Header("UI_SFX")]
    [field: SerializeField] public EventReference Click { get; private set; }
    [field: SerializeField] public EventReference ClickMaquinita { get; private set; }


    public static AudioManager instance { get; private set; }
    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("hay más de un AudioManger");
        }
        instance = this;//aaa
    }


}
