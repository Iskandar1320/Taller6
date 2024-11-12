using UnityEngine;
using TMPro;

public class GameManagerAtrapar : MonoBehaviour
{
    public TextMeshProUGUI scoreAzul;
    public TextMeshProUGUI scoreRojo;
    private int PuntosAzul;
    private int PuntosRojo;
    public GameObject GanaRojo;
    public GameObject GanaAzul;
    private SceneTransitions _sceneTransitions;
    bool gana;
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
        if(PuntosRojo == 15 && gana == false)
        {
            gana = true;
            spawnerPuntos.isSpawning = false;
            GanaRojo.SetActive(true);
            StartCoroutine(_sceneTransitions.EndScene());
        }
    }
    public void PuntoAzul()
    {
        PuntosAzul++;
        UpdateScoreText();
        if (PuntosAzul == 15 && gana == false)
        {
            gana = true;
            spawnerPuntos.isSpawning = false;

            GanaAzul.SetActive(true);
            StartCoroutine(_sceneTransitions.EndScene());
        }
    }
    private void UpdateScoreText()
    {
        scoreAzul.text = PuntosAzul.ToString();
        scoreRojo.text = PuntosRojo.ToString();
    }
}
