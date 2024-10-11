using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Video;

namespace Panel
{
    public class PanelManagement : MonoBehaviour
    {
        public RectTransform RedPanel;
        Vector2 targetPanelPos = new Vector2(1, 0);
        public Image panelColor;
        //public Color endColor;
        [SerializeField] GameObject botonPantalla;
        [SerializeField] TextMeshProUGUI toca;
        private TextMeshPro _textMeshPro;
        
       // Lista de GameObjects para las introducciones
        [SerializeField] private List<GameObject> introPanels;
        private Dictionary<GameObject, Vector3> originalScales; // Para guardar el tamaño original de cada popup
        
        
        [SerializeField] private VideoPlayer videoPlayer;
        
        
        private void Start()
        {
            _textMeshPro = GetComponent<TextMeshPro>();
            
              originalScales = new Dictionary<GameObject, Vector3>();
            
         // Inicializamos cada introPanel y guardamos su escala original
         foreach (GameObject intro in introPanels)
         {
            originalScales[intro] = intro.transform.localScale;
            intro.transform.localScale = Vector3.zero; // Los ocultamos al inicio
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
        public void ShowPopupVod(GameObject panelToShow)
        {
            if (introPanels.Contains(panelToShow))
            {
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
        public void ClosePopupVod(GameObject panelToClose)
        {
            if (introPanels.Contains(panelToClose))
            {
                panelToClose.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);
                videoPlayer.Stop();
    
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

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        public void PlayVideo()
        {
            // Asegúrate de que el VideoPlayer esté listo
            videoPlayer.Prepare();
            videoPlayer.prepareCompleted += (VideoPlayer vp) => {
                vp.Play(); // Reproducir el video una vez preparado
            };
        }
        
        private void OnDisable()
        {
            DOTween.KillAll();
        }
    }
}
