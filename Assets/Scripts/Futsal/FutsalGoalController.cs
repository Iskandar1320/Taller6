using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutsalGoalController : MonoBehaviour
{

    [SerializeField] bool blueGoal;
    [SerializeField] bool redGoal;
    // Start is called before the first frame update
    private FutsalGameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<FutsalGameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            Debug.Log("Bola en porteria");
            if (blueGoal)
            {
                gameManager.GoalScored("red"); // Gol marcado por el equipo rojo
            }
            else if (redGoal)
            {
                gameManager.GoalScored("blue"); // Gol marcado por el equipo azul
                Debug.Log("Bola en rojo");

            }
        }
    }
}
