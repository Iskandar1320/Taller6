using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FutsalGameManager : MonoBehaviour
{
    public bool redGoal;
    public bool blueGoal;

    [SerializeField] GameObject ball;

    [Header("Red Team")]
    [SerializeField] int redScore = 0;   // Puntuación del equipo rojo
    [SerializeField] GameObject redPlayers;

    [Header("Blue Team")]
    [SerializeField] int blueScore = 0;  // Puntuación del equipo azul
    [SerializeField] GameObject bluePlayers;

    [Header("Score Settings")]
    [SerializeField] TextMeshProUGUI bluePunt;
    [SerializeField] TextMeshProUGUI redPunt;

    [Header("Other Settings")]
    [SerializeField] int maxScore = 5;
    [SerializeField] float restartDelay = 2.0f;

    // Para almacenar las posiciones iniciales de cada jugador
    private Vector2[] redPlayerStartPositions;
    private Vector2[] bluePlayerStartPositions;
    private Vector2 ballStartPosition;

    private void Start()
    {
        // Guardar las posiciones iniciales de todos los jugadores
        redPlayerStartPositions = GetPlayerPositions(redPlayers);
        bluePlayerStartPositions = GetPlayerPositions(bluePlayers);
        ballStartPosition = ball.transform.position;
    }

    private void Update()
    {
        if (blueScore == maxScore)
        {
            Winner("blue");
        }

        if (redScore == maxScore)
        {
            Winner("red");
        }
    }

    public void GoalScored(string team)
    {
        if (team == "blue")
        {
            blueScore++;
            string tempScorB = blueScore.ToString();
            bluePunt.text = tempScorB;
            StartCoroutine(RestartGame());
        }
        else if (team == "red")
        {
            redScore++;
            string tempScorR = redScore.ToString();
            redPunt.text = tempScorR;
            StartCoroutine(RestartGame());
        }
    }

    private void Winner(string winner)
    {
        // Lógica para manejar la victoria
    }

    // Reiniciar el juego
    private IEnumerator RestartGame()
    {
        // Reiniciar las posiciones de jugadores y pelota
        ResetGame();

        // Esperar antes de que el jugador pueda continuar el juego
        yield return new WaitForSeconds(restartDelay);

        // Aquí puedes añadir la lógica para permitir que el juego continúe
    }

    private void ResetGame()
    {
        Debug.Log("ResetGame");

        // Reiniciar la posición de la pelota
        ball.transform.position = ballStartPosition;
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;  // Detener cualquier movimiento de la pelota

        // Reiniciar las posiciones de los jugadores
        ResetPlayerPositions(redPlayers, redPlayerStartPositions);
        ResetPlayerPositions(bluePlayers, bluePlayerStartPositions);
    }

    // Guardar las posiciones iniciales de todos los jugadores
    private Vector2[] GetPlayerPositions(GameObject players)
    {
        Vector2[] startPositions = new Vector2[players.transform.childCount];
        int index = 0;

        foreach (Transform player in players.transform)
        {
            startPositions[index] = player.position;
            index++;
        }

        return startPositions;
    }

    // Función para reiniciar las posiciones de los jugadores
    private void ResetPlayerPositions(GameObject players, Vector2[] startPositions)
    {
        int index = 0;
        foreach (Transform player in players.transform)
        {
            player.position = startPositions[index];  // Asigna la posición inicial a cada jugador
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;  // Detener el movimiento del jugador
            }
            index++;
        }
    }
}
