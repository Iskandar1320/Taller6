using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Importa el espacio de nombres de TextMeshPro

public class ScoreZone : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText; // Cambia Text a TextMeshProUGUI
    [SerializeField] GameObject pantallaFin;

    int score;

    private void Start()
    {
        score = 0;
        scoreText.text = score.ToString(); // Inicializa el texto de puntuación
    }

    private void OnCollisionEnter2D(Collision2D colCol)
    {
        if (colCol.gameObject.GetComponent<ControladorPelota>() != null)
        {
            score++;
            scoreText.text = score.ToString(); // Actualiza el texto con la puntuación
            colCol.gameObject.GetComponent<ControladorPelota>().ReinicioPelota();
            
        }
        if (score >= 7)
        {
            colCol.gameObject.SetActive(false);
            pantallaFin.SetActive(true);
        }

    }
}
