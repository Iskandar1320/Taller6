using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Importa el espacio de nombres de TextMeshPro


public class ScoreZone : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private GameObject barra1;
    [SerializeField] private GameObject barra2;
    [SerializeField] private GameObject pelota;
    [Space(1)]
    [Header("Zona de Anotación")]
    [SerializeField] private bool rojo;
    [SerializeField] private bool azul;
    [Header("Pantalla Ganador")]
    [SerializeField] private GameObject panelRojoGana;
    [SerializeField] private GameObject panelAzulGana;

    
     private SceneTransitions sceneMenu;
     private int score;

    private void Start()
    {
        sceneMenu = FindObjectOfType<SceneTransitions>();
        score = 0;
        scoreText.text = score.ToString();
    }

    private void OnCollisionEnter2D(Collision2D golpePelota)
    {
        if (golpePelota.gameObject.GetComponent<ControladorPelota>() != null)
        {
            score++;
            scoreText.text = score.ToString();
           // pelota.gameObject.GetComponent<ControladorPelota>().ReinicioPelota();
        }

        if (score >= 7 && azul==true)
        {
            StartCoroutine(WaitToDestroyAzul());
        }
        if (score>= 7 && rojo == true)
        {
            StartCoroutine(WaitToDestroyRojo());
        }
 
    }
    private IEnumerator WaitToDestroyAzul()
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(pelota);
        barra1.SetActive(false);
        barra2.SetActive(false);
        panelAzulGana.SetActive(true);
        StartCoroutine(WaitToChangeScene());
    }
    private IEnumerator WaitToDestroyRojo()
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(pelota);
        barra1.SetActive(false);
        barra2.SetActive(false);
        panelRojoGana.SetActive(true);
        StartCoroutine(WaitToChangeScene());
    }
    private IEnumerator WaitToChangeScene()
    {
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(sceneMenu.EndScene());
    }
}
