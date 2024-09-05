using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class ContadorDeColisiones : MonoBehaviour
{
    GameManagerContar gameManager;
    private int diferencia1;
    private int diferencia2;
    public int contador = 0;
    public int contAzul = 0;
    public int contRojo = 0;
    private float tiempoDeEspera = 35f;
    public GameObject AzulGana;
    public GameObject RojoGana;
    public GameObject Empate;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI contAzulTXT;
    public TextMeshProUGUI contRojoTXT;
    public TextMeshProUGUI contadorTXT;
    public bool GameFinished = false;

    private void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManagerContar>();
        contAzulTXT.enabled = false;
        contRojoTXT.enabled = false;
        contadorTXT.enabled = false;
    }
    private void Update()
    {
        contAzulTXT.text = contAzul.ToString();
        contRojoTXT.text = contRojo.ToString();
        contadorTXT.text = "Total pajaros amarillos: " + contador.ToString();


        if (gameManager.gameStarted == true)
        {
            StartCoroutine(EjecutarAccionDespuesDeTiempo());
            if (tiempoDeEspera > 0)
            {
                tiempoDeEspera -= Time.deltaTime;
                if (tiempoDeEspera < 0)
                    tiempoDeEspera = 0;
            }
            else
            {
                contAzulTXT.enabled = true;
                contRojoTXT.enabled = true;
                contadorTXT.enabled = true;
                GameFinished = true;
            }

            int secondsRemaining = Mathf.FloorToInt(tiempoDeEspera);
            timeText.text =secondsRemaining.ToString();
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PjroAma"))
        {
            contador++;
        }
    }
    public void Azul()
    {
        contAzul++;
        CompararContadores();
    }
    public void Rojo()
    {
        contRojo++;
        CompararContadores();
    }
    private void CompararContadores()
    {
        diferencia1 = Mathf.Abs(contador - contAzul);
        diferencia2 = Mathf.Abs(contador - contRojo);
    }


    private IEnumerator EjecutarAccionDespuesDeTiempo()
    {
        yield return new WaitForSeconds(tiempoDeEspera);    

        if (diferencia1 < diferencia2)
        {
            Debug.Log("Azul gana");
            AzulGana.SetActive(true);
        }
        else if (diferencia2 < diferencia1)
        {
            Debug.Log("Rojo gana");
            RojoGana.SetActive(true);
        }
        else
        {
            Debug.Log("Empate");
            Empate.SetActive(true);
        }
    }
}
