using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{
    [Header("Volume")]
    [Range(0, 1)]
    public float masterVolume = 1;
    [Range(0, 1)]
    public float musicVolume = 1;
    [Range(0, 1)]
    public float sfxVolume = 1;
    [Range(0, 1)]

    private Bus masterBus;
    private Bus musicBus;
    private Bus sfxBus;

    public static AudioManager Instance; // { get; private set; }

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

        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        sfxBus = RuntimeManager.GetBus("bus:/SFX");


    }
    private void Update()
    {
        masterBus.setVolume(masterVolume);
        musicBus.setVolume(musicVolume);
        sfxBus.setVolume(sfxVolume);
    }
    /*private void Start()
    {
        // Asegura que Fmod_Events.instance esté inicializado
        if (Fmod_Events.instance == null)
        {
            Debug.Log("Buscando Fmod_Events en Start de AudioManager...");
            Fmod_Events.instance = GetComponentInChildren<Fmod_Events>();

            if (Fmod_Events.instance == null)
            {
                Debug.LogError("Fmod_Events no se encontró como hijo de AudioManager.");
            }
            else
            {
                Debug.Log("Fmod_Events encontrado y asignado correctamente.");
            }
        }
    }*/
    public void PlayOneShot(EventReference sound)
    {
        RuntimeManager.PlayOneShot(sound);
    }
    public void ClickMaquina()
    {
        PlayOneShot(Fmod_Events.Instance.ClickMaquinita);
    }
    public void ClickUi()
    {
        RuntimeManager.PlayOneShot(Fmod_Events.Instance.Click);
    }
    public void ClickPajaros()
    {
        RuntimeManager.PlayOneShot(Fmod_Events.Instance.explotion);
    }
}
