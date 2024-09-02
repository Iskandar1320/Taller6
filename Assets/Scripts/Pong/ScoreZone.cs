using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreZone : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] GameObject pantallaFin;

    int score;

    private void Start()
    {
        score = 0;
    }

    private void OnCollisionEnter2D(Collision2D colCol)
    {
        if (colCol.gameObject.GetComponent<ControladorPelota>() != null) 
        {
            score++;
            scoreText.text = score.ToString();
            colCol.gameObject.GetComponent<ControladorPelota>().ReinicioPelota();
        }
        if(score >= 7)
        {
            colCol.gameObject.SetActive(false);
            pantallaFin.SetActive(true);
        }
        
    }
}
