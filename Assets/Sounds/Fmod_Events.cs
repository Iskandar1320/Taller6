using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fmod_Events : MonoBehaviour
{
    public static Fmod_Events Instance { get; private set; }

    [field: Header("SFX")]
    [field: SerializeField] public EventReference cartasOpen { get; private set; }
    [field: SerializeField] public EventReference cartasClose { get; private set; }
    [field: SerializeField] public EventReference crowd { get; private set; }
    [field: SerializeField] public EventReference gunShot { get; private set; }
    [field: SerializeField] public EventReference golpePelota { get; private set; }
    [field: SerializeField] public EventReference Winning { get; private set; }
    [field: SerializeField] public EventReference explotion { get; private set; }
    [field: SerializeField] public EventReference caida { get; private set; }
    [field: SerializeField] public EventReference empuje { get; private set; }
    [field: SerializeField] public EventReference spawnPajaro { get; private set; }
    [field: SerializeField] public EventReference eat { get; private set; }
    [field: SerializeField] public EventReference FutsalKickBall { get; private set; }
    [field: SerializeField] public EventReference Whistle { get; private set; }
    [field: SerializeField] public EventReference correctGood { get; private set; }


    [field: Header("UI_SFX")]
    [field: SerializeField] public EventReference Click { get; private set; }
    [field: SerializeField] public EventReference ClickMaquinita { get; private set; }
    private void Awake()
    {
        // Si ya existe una instancia y no es esta, destruye este objeto
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Asigna esta instancia y haz que persista entre escenas
        Instance = this;
        DontDestroyOnLoad(gameObject); // Hace que no se destruya al cargar otra escena
    }


}
