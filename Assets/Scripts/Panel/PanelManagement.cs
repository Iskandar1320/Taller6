using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

        private void Start()
        {
            _textMeshPro = GetComponent<TextMeshPro>();
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
        private void OnDisable()
        {
            DOTween.KillAll();
        }
    }
}
