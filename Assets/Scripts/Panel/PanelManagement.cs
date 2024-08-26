using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
public class PanelManagement : MonoBehaviour
{
    public RectTransform RedPanel;
    Vector2 targetPanelPos = new Vector2(1, 0);
    public Image panelColor;
    //public Color endColor;
    [SerializeField] GameObject botonPantalla;
    public void ChangeScene(string _sceneName) 
    {
        SceneManager.LoadScene(_sceneName);
    }
    
    public void MovePanel()
    {
        // RedPanel.DOAnchorPos(targetPanelPos, 1).SetEase(Ease.Flash).SetDelay(0.1f);
        RedPanel.DOAnchorPos(targetPanelPos, 1).SetEase(Ease.OutBounce);
        panelColor.DOFade(1, 1f);
        botonPantalla.SetActive(false);
    }
}
