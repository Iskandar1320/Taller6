using System;
using System.Collections;
using System.ComponentModel;
using TMPro;
using UnityEngine;

namespace Contar
{
    public class ContadorDeColisiones : MonoBehaviour
    {
        GameManagerContar gameManager;
        private int diferencia1;
        private int diferencia2;
        public int contador = 0;
        public int contAzul = 0;
        public int contRojo = 0;
        private float tiempoDeRonda = 10f;
        public GameObject AzulGana;
        public GameObject RojoGana;
        public GameObject Empate;
        public TextMeshProUGUI timeText;
        public TextMeshProUGUI contAzulTXT;
        public TextMeshProUGUI contRojoTXT;
        public TextMeshProUGUI contadorTXT;

        public int rondaAzul = 0;
        public int rondaRojo = 0;

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

                if (tiempoDeRonda > 0)
                {
                    tiempoDeRonda -= Time.deltaTime;
                    if (tiempoDeRonda < 0)
                        tiempoDeRonda = 0;
                }
                else
                {
                    gameManager.CancelInvoke();
                    contAzulTXT.enabled = true;
                    contRojoTXT.enabled = true;
                    contadorTXT.enabled = true;

                    if (diferencia1 < diferencia2)
                    {
                        rondaRojo++;
                        if (rondaRojo == 2)
                        {
                            RojoGana.SetActive(true);
                        }
                        else
                        {
                            Debug.Log("ronda rojo");
                            EjecutarAccionDespuesDeRonda();
                        }
                        return;
                    }
                    else if (diferencia2 < diferencia1)
                    {
                        rondaAzul++;
                        if (rondaAzul == 2)
                        {
                            AzulGana.SetActive(true);
                        }
                        else
                        {
                            Debug.Log("ronda Azul");
                            EjecutarAccionDespuesDeRonda();
                        }
                        return;
                    }
                    else
                    {
                        EjecutarAccionDespuesDeRonda();
                        return;
                    }
                }

                int secondsRemaining = Mathf.FloorToInt(tiempoDeRonda);
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
            yield return new WaitForSeconds(tiempoDeRonda);        
        }
        private IEnumerator EjecutarAccionDespuesDeRonda()
        {
            yield return new WaitForSeconds(3f);
            contAzul = 0;
            contRojo = 0;
            contador = 0;
            diferencia1 = 0;
            diferencia2 = 0;
            gameManager.StartSpawning();
        }
    }
}
