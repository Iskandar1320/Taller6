using UnityEngine;
using TMPro;

public class GameManagerAtrapar : MonoBehaviour
{
    public TextMeshProUGUI scoreAzul;
    public TextMeshProUGUI scoreRojo;
    private int PuntosAzul = 0;
    private int PuntosRojo = 0;
    public GameObject GanaRojo;
    public GameObject GanaAzul;
    private SceneTransitions _sceneTransitions;
    bool gana = false;
    SpawnerPuntos spawnerPuntos;

    private void Start()
    {
       _sceneTransitions = FindObjectOfType<SceneTransitions>();
        spawnerPuntos = GameObject.FindObjectOfType<SpawnerPuntos>();
    }
    public void inicio()
    {
        spawnerPuntos.isSpawning = true;
    }

    public void PuntoRojo()
    {
        PuntosRojo++;
        UpdateScoreText();
        if(PuntosRojo == 5 && gana == false)
        {
            gana = true;
            spawnerPuntos.isSpawning = false;
            GanaRojo.SetActive(true);
            print("ola");
            StartCoroutine(_sceneTransitions.EndScene());
        }
    }
    public void PuntoAzul()
    {
        PuntosAzul++;
        UpdateScoreText();
        if (PuntosAzul == 5 && gana == false)
        {
            gana = true;
            spawnerPuntos.isSpawning = false;

            GanaAzul.SetActive(true);
            print("ola");
            
            StartCoroutine(_sceneTransitions.EndScene());
        }
    }
    private void UpdateScoreText()
    {
        scoreAzul.text = PuntosAzul.ToString();
        scoreRojo.text = PuntosRojo.ToString();
    }
}
