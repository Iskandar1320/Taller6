using System.Collections;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Build.Reporting;
using UnityEngine.UI;

public class FutsalGameManager : MonoBehaviour
{
    public bool redGoal;
    public bool blueGoal;

    [SerializeField] GameObject ball;

    [Header("Red Team")]
    [SerializeField] int redScore = 0;   // Puntuaci�n del equipo rojo
    [SerializeField] GameObject redPlayers;

    [Header("Blue Team")]
    [SerializeField] int blueScore = 0;  // Puntuaci�n del equipo azul
    [SerializeField] GameObject bluePlayers;

    [Header("Score Settings")]
    [SerializeField] private bool ganaRed;
    [SerializeField] private bool ganaBlue;
    
    [SerializeField] TextMeshProUGUI bluePunt;
    [SerializeField] TextMeshProUGUI redPunt;
    [SerializeField] TextMeshProUGUI winText;
    [SerializeField] GameObject winPannel;
    [SerializeField] private Image colorpanel;



    [Header("Other Settings")]
    [SerializeField] int maxScore = 5;
    [SerializeField] float restartDelay = 2.0f;
    [SerializeField] float _startTransitionDelay = 4.0f;

    // Para almacenar las posiciones iniciales de cada jugador
    private Vector2[] redPlayerStartPositions;
    private Vector2[] bluePlayerStartPositions;
    private Vector2 ballStartPosition;
    private SceneTransitions _sceneTransitions;

    private void Start()
    {
        _sceneTransitions = FindObjectOfType<SceneTransitions>();

        // Guardar las posiciones iniciales de todos los jugadores
        redPlayerStartPositions = GetPlayerPositions(redPlayers);
        bluePlayerStartPositions = GetPlayerPositions(bluePlayers);
        ballStartPosition = ball.transform.position;
        winPannel.SetActive(false);
        StartCoroutine(StartGame());    
       
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

        TestWin();
    }
    /// <summary>
    /// Esto es para poner la pantalla de Win visualmente
    /// </summary>
    private void TestWin()
    {
        if (ganaRed)
        {
            winPannel.SetActive(true);
            colorpanel.color = new Color32(161, 28, 28, 233);
            winText.text = "Player 1 Wins";
        }
        else if (ganaBlue)
        {
            winPannel.SetActive(true);
            colorpanel.color = new Color32(28, 39, 161, 233);
            winText.text = "Player 2 Wins";
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
        winPannel.SetActive(true);
        if (winner == "blue")
        {
            winText.text = "Player 2 Wins";
            colorpanel.color = new Color32(28, 39, 161, 233);

        }
        else if (winner == "red")
        {
            winText.text = "Player 1 Wins";
            colorpanel.color = new Color32(161, 28, 28, 233);

        }
        StartCoroutine(_sceneTransitions.EndScene());
        // L�gica para manejar la victoria
    }

    // Reiniciar el juego
    private IEnumerator RestartGame()
    {
        // Activar el panel y mostrar la cuenta regresiva en el winText
        winPannel.SetActive(true);

        for (float timer = restartDelay; timer > 0; timer -= Time.deltaTime)
        {
            winText.text = "" + Mathf.Ceil(timer).ToString();  // Mostrar cuenta regresiva
            yield return null;  // Esperar un frame
        }

        // Reiniciar las posiciones de jugadores y pelota
        ResetGame();

        // Desactivar el panel y el texto despu�s del tiempo de reinicio
        winPannel.SetActive(false);
        winText.text = "";  // Limpiar el texto
    }

    private IEnumerator StartGame()
    {
        winPannel.SetActive(true);

        for (float timer = _startTransitionDelay; timer >0; timer -=Time.deltaTime)
        {
            winText.text = "" + Mathf.Ceil(timer).ToString();  // Mostrar cuenta regresiva
            yield return null;
        }
        ResetGame();
        winPannel.SetActive(false);
        winText.text = "";
    }

    private void ResetGame()
    {
        Debug.Log("ResetGame");

        // Reiniciar la posici�n de la pelota
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

    // Funci�n para reiniciar las posiciones de los jugadores
    private void ResetPlayerPositions(GameObject players, Vector2[] startPositions)
    {
        int index = 0;
        foreach (Transform player in players.transform)
        {
            player.position = startPositions[index];  // Asigna la posici�n inicial a cada jugador
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = Vector2.zero;  // Detener el movimiento del jugador
            }
            index++;
        }
    }
}
