using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class FutsalGameManager : MonoBehaviour
{
    public bool redGoal;
    public bool blueGoal;

    [Header("Joysticks")]
    [SerializeField] GameObject blueJoystick;
    [SerializeField] GameObject redJoystick;

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

    [Header("Resolution Seettings")]
    [SerializeField] GameObject ipadBackground;
    [SerializeField] GameObject iphoneBackground;
    [SerializeField] GameObject northSouthBoundaries;
    [SerializeField] GameObject redGoalEdgeNorth;
    [SerializeField] GameObject redGoalEdgeSouth;
    [SerializeField] GameObject blueGoalEdgeNorth;
    [SerializeField] GameObject blueGoalEdgeSouth;
    [SerializeField] GameObject playerEdges;


    [Header("Other Settings")]
    [SerializeField] int maxScore = 5;
    [SerializeField] float restartDelay = 2.0f;
    [SerializeField] float _startTransitionDelay = 4.0f;

    [SerializeField] private FutsalPlayerController redPlayerController;  // Referencia para el controlador del equipo rojo
    [SerializeField] private FutsalPlayerController bluePlayerController;  // Referencia para el controlador del equipo azul

    [Header("Audio Sources")]
    [SerializeField] AudioSource resetGameSound;
    [SerializeField] AudioSource winnerSound;
    [SerializeField] AudioSource goalScoredSound;

    // Para almacenar las posiciones iniciales de cada jugador
    private Vector2[] redPlayerStartPositions;
    private Vector2[] bluePlayerStartPositions;
    private Vector2 ballStartPosition;
    private SceneTransitions _sceneTransitions;

    private float strikerMinX;
    private float strikerMaxX;
    private float strikerMinY;
    private float strikerMaxY;
    private float goalkeeperMinY;
    private float goalkeeperMaxY;



    private void Start()
    {
        _sceneTransitions = FindObjectOfType<SceneTransitions>();

        // Guardar las posiciones iniciales de todos los jugadores
        redPlayerStartPositions = GetPlayerPositions(redPlayers);
        bluePlayerStartPositions = GetPlayerPositions(bluePlayers);
        ballStartPosition = ball.transform.position;
        winPannel.SetActive(false);
        StartCoroutine(StartGame());
        strikerMinX = -13.45f;
        strikerMaxX = 13.45f;
        strikerMinY = -6.2f;
        strikerMaxY = 6.2f;

        goalkeeperMinY = -3.5f;
        goalkeeperMaxY = 3.5f;
        resolution();
        
    }

    private void Awake()
    {
        blueJoystick.gameObject.SetActive(false);
        redJoystick.gameObject.SetActive(false);
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
    /// 

    private void resolution()
    {
        float aspectRatio = (float)Screen.height / Screen.width;
        print(aspectRatio);

        // Definir una tolerancia mayor para la comparación
        float iphoneTolerance = 0.1f;
        float ipadTolerance = 0.2f;

        // Si el aspecto es cercano a 16:9 (~1.78)
        if (Mathf.Abs(aspectRatio - 1.78f) <= iphoneTolerance)
        {
            ipadBackground.SetActive(false);
            iphoneBackground.SetActive(true);

            // Ajustes de escala y posición para 16:9
            northSouthBoundaries.transform.localScale = new Vector3(4.3f, blueGoalEdgeSouth.transform.localScale.y, northSouthBoundaries.transform.localScale.z);

            redGoalEdgeNorth.transform.localScale = new Vector3(4.3f, redGoalEdgeNorth.transform.localScale.y, redGoalEdgeNorth.transform.localScale.z);
            redGoalEdgeNorth.transform.position = new Vector3(-14.126f, 4.67f, redGoalEdgeNorth.transform.position.z);

            redGoalEdgeSouth.transform.localScale = new Vector3(4.3f, redGoalEdgeSouth.transform.localScale.y, redGoalEdgeSouth.transform.localScale.z);
            redGoalEdgeSouth.transform.position = new Vector3(-14.126f, -4.67f, redGoalEdgeSouth.transform.position.z);

            blueGoalEdgeNorth.transform.localScale = new Vector3(4.3f, blueGoalEdgeNorth.transform.localScale.y, blueGoalEdgeNorth.transform.localScale.z);
            blueGoalEdgeNorth.transform.position = new Vector3(14.126f, 4.67f, blueGoalEdgeNorth.transform.position.z);

            blueGoalEdgeSouth.transform.localScale = new Vector3(4.3f, blueGoalEdgeSouth.transform.localScale.y, blueGoalEdgeSouth.transform.localScale.z);
            blueGoalEdgeSouth.transform.position = new Vector3(14.126f, -4.67f, blueGoalEdgeSouth.transform.position.z);

            playerEdges.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

            // Ajuste de los Joysticks
            blueJoystick.transform.localScale = new Vector3(0.834f, 0.834f, 1.0f);
            redJoystick.transform.localScale = new Vector3(0.834f, 0.834f, 1.0f);

            // Ajustes de rango de movimiento para 16:9
            strikerMinX = -13.45f;
            strikerMaxX = 13.45f;
            strikerMinY = -6.2f;
            strikerMaxY = 6.2f;

            goalkeeperMinY = -3.5f;
            goalkeeperMaxY = 3.5f;
        }
        // Si el aspecto es cercano a 4:3 (~1.33333 para iPad)
        else if (Mathf.Abs(aspectRatio - 1.33333f) <= ipadTolerance)
        {
            ipadBackground.SetActive(true);
            iphoneBackground.SetActive(false);

            // Ajustes de escala y posición para 4:3
            northSouthBoundaries.transform.localScale = new Vector3(northSouthBoundaries.transform.localScale.x, 1.4f, northSouthBoundaries.transform.localScale.z);

            redGoalEdgeNorth.transform.localScale = new Vector3(6.9f, redGoalEdgeNorth.transform.localScale.y, redGoalEdgeNorth.transform.localScale.z);
            redGoalEdgeNorth.transform.position = new Vector3(-14.13f, 6.07f, redGoalEdgeNorth.transform.position.z);

            redGoalEdgeSouth.transform.localScale = new Vector3(6.9f, redGoalEdgeSouth.transform.localScale.y, redGoalEdgeSouth.transform.localScale.z);
            redGoalEdgeSouth.transform.position = new Vector3(-14.13f, -6.07f, redGoalEdgeSouth.transform.position.z);

            blueGoalEdgeNorth.transform.localScale = new Vector3(6.9f, blueGoalEdgeNorth.transform.localScale.y, blueGoalEdgeNorth.transform.localScale.z);
            blueGoalEdgeNorth.transform.position = new Vector3(14.13f, 6.07f, blueGoalEdgeNorth.transform.position.z);

            blueGoalEdgeSouth.transform.localScale = new Vector3(6.9f, blueGoalEdgeSouth.transform.localScale.y, blueGoalEdgeSouth.transform.localScale.z);
            blueGoalEdgeSouth.transform.position = new Vector3(14.13f, -6.07f, blueGoalEdgeSouth.transform.position.z);

            playerEdges.transform.localScale = new Vector3(1.0f, 1.4f, 1.0f);

            // Ajuste de los Joysticks
            blueJoystick.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            redJoystick.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);

            // Ajustes de rango de movimiento para 4:3
            strikerMinX = -13.45f;
            strikerMaxX = 13.45f;
            strikerMinY = -8.9f;
            strikerMaxY = 8.9f;

            goalkeeperMinY = -3.5f;
            goalkeeperMaxY = 3.5f;
        }
        if (redPlayerController != null)
        {
            redPlayerController.StrikerMinX = strikerMinX;
            redPlayerController.StrikerMaxX = strikerMaxX;
            redPlayerController.StrikerMinY = strikerMinY;
            redPlayerController.StrikerMaxY = strikerMaxY;
            redPlayerController.GoalkeeperMinY = goalkeeperMinY;
            redPlayerController.GoalkeeperMaxY = goalkeeperMaxY;
        }

        if (bluePlayerController != null)
        {
            bluePlayerController.StrikerMinX = strikerMinX;
            bluePlayerController.StrikerMaxX = strikerMaxX;
            bluePlayerController.StrikerMinY = strikerMinY;
            bluePlayerController.StrikerMaxY = strikerMaxY;
            bluePlayerController.GoalkeeperMinY = goalkeeperMinY;
            bluePlayerController.GoalkeeperMaxY = goalkeeperMaxY;
        }
    }


    // Método auxiliar para ajustar la escala de los objetos

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
            goalScoredSound.Play();
            StartCoroutine(RestartGame());
        }
        else if (team == "red")
        {
            redScore++;
            string tempScorR = redScore.ToString();
            redPunt.text = tempScorR;
            goalScoredSound.Play();
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
        winnerSound.Play();
        StartCoroutine(_sceneTransitions.EndScene());
        // L�gica para manejar la victoria
    }

    // Reiniciar el juego
    private IEnumerator RestartGame()
    {
        // Activar el panel y mostrar la cuenta regresiva en el winText
        winPannel.SetActive(true);
        blueJoystick.gameObject.SetActive(false);
        redJoystick.gameObject.SetActive(false);
        if (blueScore < maxScore && redScore < maxScore)
        {
            for (float timer = restartDelay; timer > 0; timer -= Time.deltaTime)
            {
                winText.text = "" + Mathf.Ceil(timer).ToString();  // Mostrar cuenta regresiva
                yield return null;  // Esperar un frame
            }
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

        // Activa los Joysticks

        blueJoystick.gameObject.SetActive(true);
        redJoystick.gameObject.SetActive(true);

        // Reiniciar la posici�n de la pelota
        ball.transform.position = ballStartPosition;
        ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;  // Detener cualquier movimiento de la pelota

        // Reiniciar las posiciones de los jugadores
        ResetPlayerPositions(redPlayers, redPlayerStartPositions);
        ResetPlayerPositions(bluePlayers, bluePlayerStartPositions);
        if (blueScore < maxScore && redScore < maxScore)
        {
            resetGameSound.Play();

        }
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
