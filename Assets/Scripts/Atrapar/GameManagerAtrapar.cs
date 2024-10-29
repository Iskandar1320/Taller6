using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManagerAtrapar : MonoBehaviour
{
    public TextMeshProUGUI scoreAzul;
    public TextMeshProUGUI scoreRojo;
    private int PuntosAzul = 0;
    private int PuntosRojo = 0;
    public void PuntoRojo()
    {
        PuntosRojo++;
        UpdateScoreText();
    }
    public void PuntoAzul()
    {
        PuntosAzul++;
        UpdateScoreText();
    }
    private void UpdateScoreText()
    {
        scoreAzul.text = PuntosAzul.ToString();
        scoreRojo.text = PuntosRojo.ToString();
    }
}
