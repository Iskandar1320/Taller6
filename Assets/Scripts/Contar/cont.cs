using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        public TextMeshProUGUI timeText;
        public TextMeshProUGUI contAzulTXT;
        public TextMeshProUGUI contRojoTXT;
        public TextMeshProUGUI contadorTXT;
        public int rondaAzul = 0;
        public int rondaRojo = 0;
        public bool finRonda = false;
        public GameObject puntoAzul;
        public GameObject puntoRojo;
        private Button BtnRojo;
        private Button BtnAzul;
    
        private SceneTransitions _sceneTransitions;
        private bool gana;
        [SerializeField] GameObject tapToStart;
        [SerializeField] private AudioSource winSound;


        private void Start()
        {
            BtnRojo = GameObject.Find("Rojo").GetComponent<Button>();
            BtnAzul = GameObject.Find("Azul").GetComponent<Button>();

            _sceneTransitions = FindObjectOfType<SceneTransitions>();
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

           
            if (gameManager.gameStarted)
            {
                tapToStart.SetActive(false);
                StartCoroutine(EjecutarAccionDespuesDeTiempo());

                if (tiempoDeRonda > 0 && finRonda == false)
                {
                    tiempoDeRonda -= Time.deltaTime;
                    if (tiempoDeRonda < 0)
                    {
                        tiempoDeRonda = 0;
                        finRonda = true;
                    }
                }
                else if (finRonda)
                {
                    gameManager.isSpawning = false;
                    BtnAzul.interactable = false;
                    BtnRojo.interactable = false;
                    CompararContadores();
                    contAzulTXT.enabled = true;
                    contRojoTXT.enabled = true;
                    contadorTXT.enabled = true;

                    if (diferencia1 > diferencia2)
                    {
                        rondaRojo++;
                        if (rondaRojo >= 2 && gana == false)
                        {
                            gameManager.isSpawning = false;
                            puntoAzul.SetActive(false);
                            puntoRojo.SetActive(false);
                            RojoGana.SetActive(true);
                            winSound.Play();

                            gana = true;
                            StartCoroutine(_sceneTransitions.EndScene());
                            return;
                        }
                        else
                        {

                            puntoRojo.SetActive(true);
                            StartCoroutine(EjecutarAccionDespuesDeRonda());
                            finRonda = false;
                        }
                        return;
                    }
                    else if (diferencia1 < diferencia2)
                    {
                        rondaAzul++;
                        if (rondaAzul >= 2 && gana == false)
                        {
                            gameManager.isSpawning = false;
                            puntoAzul.SetActive(false);
                            puntoRojo.SetActive(false);
                            AzulGana.SetActive(true);
                            winSound.Play();

                            gana = true;
                            StartCoroutine(_sceneTransitions.EndScene());
                        }
                        else
                        {
                            puntoAzul.SetActive(true);
                            StartCoroutine(EjecutarAccionDespuesDeRonda());
                            finRonda = false;
                        }
                        return;
                    }
                    else
                    {
                        StartCoroutine(EjecutarAccionDespuesDeRonda());
                        finRonda = false;
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
        }
        public void Rojo()
        {
            contRojo++;
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
            tiempoDeRonda = 10f;
            gameManager.AudioRonda();
            contAzulTXT.enabled = false;
            contRojoTXT.enabled = false;
            contadorTXT.enabled = false;
            contAzul = 0;
            contRojo = 0;
            contador = 0;
            diferencia1 = 0;
            diferencia2 = 0;
            BtnAzul.interactable = true;
            BtnRojo.interactable = true;
            gameManager.ReiniciarSpawnRates();
            gameManager.isSpawning = true;
        }
    }
}
