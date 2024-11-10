using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Video;
using System.Collections;

namespace Panel
{
    public class PanelManagement : MonoBehaviour
    {
        public RectTransform RedPanel;
        Vector2 targetPanelPos = new Vector2(1, 0);
        //public Color endColor;
        [SerializeField] GameObject botonPantalla;
        [SerializeField] TextMeshProUGUI toca;
        private TextMeshPro _textMeshPro;
        
       // Lista de GameObjects para las introducciones
        [SerializeField] private List<GameObject> introPanels;
        [SerializeField] private List<GameObject> videoPanels;
        private Dictionary<GameObject, Vector3> originalScales; // Para guardar el tamaño original de cada popup

        [SerializeField] private GameObject panelInicial;
        [SerializeField] private GameObject panel2dario;

        private CanvasGroup canvas1 = null;
        private CanvasGroup canvas2 = null;
        private VideoPlayer videoPlayer;
        
        private void Start()
        {
            canvas1 = panelInicial.GetComponent<CanvasGroup>();
            canvas2 = panel2dario.GetComponent<CanvasGroup>();

            StartCoroutine(PantallaMenu());
            _textMeshPro = GetComponent<TextMeshPro>();
            
              originalScales = new Dictionary<GameObject, Vector3>();
            
         // Inicializamos cada introPanel y guardamos su escala original
         foreach (GameObject intro in introPanels)
         {
            originalScales[intro] = intro.transform.localScale;
            intro.transform.localScale = Vector3.zero; // Los ocultamos al inicio
         }

         foreach (GameObject video in videoPanels)
         {
             originalScales[video] = video.transform.localScale;
             video.transform.localScale = Vector3.zero; // Los ocultamos al inicio
         }
                    
        }
        
        // Método que escala el popup específico a su tamaño original cuando se presiona el botón
        public void ShowPopup(GameObject panelToShow)
        {
            if (introPanels.Contains(panelToShow))
            {
                Vector3 originalScale = originalScales[panelToShow];
                panelToShow.transform.DOScale(originalScale, 0.5f).SetEase(Ease.OutBounce);
            }
        }
        public void ShowBotonVod(GameObject panelToShow)
        {
                panelToShow.SetActive(true); // Activar el GameObject al mostrar el panel

            if (videoPanels.Contains(panelToShow))
            {

                VideoPlayer[] videoPlayers = panelToShow.GetComponentsInChildren<VideoPlayer>();

                if (videoPlayers.Length > 0)
                {
                    videoPlayer = videoPlayers[0]; // Asignar el primer VideoPlayer encontrado
                }

                Vector3 originalScale = originalScales[panelToShow];
                panelToShow.transform.DOScale(originalScale, 0.5f).SetEase(Ease.OutBounce);
                PlayVideo();
            }
        }



        public void ClosePopup(GameObject panelToClose)
        {
            if (introPanels.Contains(panelToClose))
            {
                panelToClose.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);
            }

        }

        public void CloseBotonVod(GameObject panelToClose)
        {
            if (videoPanels.Contains(panelToClose))
            {
                VideoPlayer[] videoPlayers = panelToClose.GetComponentsInChildren<VideoPlayer>();

                foreach (VideoPlayer vp in videoPlayers)
                {
                    vp.Stop(); // Asegurarse de detener cada VideoPlayer dentro del panel
                }

                panelToClose.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);

                // Desactivar el GameObject una vez que el panel se ha cerrado
                panelToClose.SetActive(false);
            }
        }
        public void ChangeScene(string _sceneName) 
        {
            SceneManager.LoadScene(_sceneName);
        }

        public void EndGameScene(int playerNumber)
        {
            _textMeshPro.text = playerNumber > 1 ? "Player 1 Wins" : "Player 2 Wins";
            if (playerNumber > 0) ChangeScene("PantallaInicio_1");

        }

        public void MovePanel()
        {
            // RedPanel.DOAnchorPos(targetPanelPos, 1).SetEase(Ease.Flash).SetDelay(0.1f);
            
            // Iniciamos la animación del panel
            RedPanel.DOAnchorPos(targetPanelPos, 1).SetEase(Ease.OutBounce)
                // Cuando la animación termine, matamos las transiciones
                .OnComplete(() => DOTween.KillAll());
            //panelColor.DOFade(1, 1f).OnComplete(() => DOTween.Kill(panelColor));

            toca.text = "";
            botonPantalla.SetActive(false);

        }
        private void TransicionarPanelIn()
        {
            canvas1.DOFade(0, 1).OnComplete(() =>
            {
                panelInicial.SetActive(false);
                panel2dario.SetActive(true);
                StartCoroutine(EsperarPanel2());
            });
            
            
        }
        private void TransicionPanelOut()
        {
            canvas2.DOFade(1, 1).OnComplete(() => DOTween.KillAll());
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void PlayVideo()
        {
            if (videoPlayer != null && videoPlayer.gameObject.activeInHierarchy)
            {
                videoPlayer.Prepare();
                videoPlayer.prepareCompleted += (VideoPlayer vp) => {
                    vp.Play(); // Reproducir el video una vez preparado
                };
            }
        }

        private void OnDisable()
        {
            DOTween.KillAll();
        }
        private IEnumerator PantallaMenu()
        {
            yield return new WaitForSeconds(2.7f);
            TransicionarPanelIn();
        }
        private IEnumerator EsperarPanel2()
        {
            TransicionPanelOut();
            yield return new WaitForSeconds(0.5f);
        }
    }
}
