using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FutsalPlayerController : MonoBehaviour
{
    


    [SerializeField] private GameObject striker;
    [SerializeField] private GameObject goalkeeper;
    [SerializeField] private Joystick joystick;
    [SerializeField] float strikerSpeed = 5.0f;
    [SerializeField] float goalkeeperSpeed = 5.0f;

    // Límites de la cancha para los Delanteros
    [SerializeField] private float strikerMinX;
    [SerializeField] private float strikerMaxX;
    [SerializeField] private float strikerMinY;
    [SerializeField] private float strikerMaxY;


    // Rango permitido para el movimiento del portero (en el eje Y)
    [SerializeField] private float goalkeeperMinY;
    [SerializeField] private float goalkeeperMaxY;

    // Booleanos para diferenciar a que equipo pertenecen esto debido a que toca invertir los ejes de movimiento dependiendo de la posicion en la pantalla
    [SerializeField] bool redTeam;
    [SerializeField] bool blueTeam;

    private Rigidbody2D strikerRb;
    private Rigidbody2D goalkeeperRb;

    private float horizontalInput;
    private float verticalInput;

    public float StrikerMinX { get => strikerMinX; set => strikerMinX = value; }
    public float StrikerMaxX { get => strikerMaxX; set => strikerMaxX = value; }
    public float StrikerMinY { get => strikerMinY; set => strikerMinY = value; }
    public float StrikerMaxY { get => strikerMaxY; set => strikerMaxY = value; }

    public float GoalkeeperMinY { get => goalkeeperMinY; set => goalkeeperMinY = value; }
    public float GoalkeeperMaxY { get => goalkeeperMaxY; set => goalkeeperMaxY = value; }

    private void Awake()
    {
        strikerRb = striker.GetComponent<Rigidbody2D>();
        goalkeeperRb = goalkeeper.GetComponent<Rigidbody2D>();
    }

    
    private void FixedUpdate()
    {
        ReadJoystickInput();

        MoveStriker();
        MoveGoalkeeper();

        RotateStriker();
    }

    private void ReadJoystickInput()
    {
        if (blueTeam == true)
        {
            horizontalInput = -joystick.Vertical;  // El eje vertical del joystick controla el movimiento horizontal
            verticalInput = joystick.Horizontal;   // El eje horizontal del joystick controla el movimiento vertical
        }
        else if (redTeam == true)
        {
            horizontalInput = -joystick.Vertical;  // El eje vertical del joystick controla el movimiento horizontal
            verticalInput = joystick.Horizontal;   // El eje horizontal del joystick controla el movimiento vertical
        }
    }

    private void MoveStriker()
    {
        if (blueTeam == true)
        {
            // Movimiento del delantero en los dos ejes (Y = vertical, X = horizontal)
            Vector2 strikerMovement = new Vector2(horizontalInput, verticalInput) * strikerSpeed;
            Vector2 newPosition = strikerRb.position + strikerMovement * Time.deltaTime;

            // Limitar el movimiento del striker dentro del área de juego
            newPosition.x = Mathf.Clamp(newPosition.x, strikerMinX, strikerMaxX);
            newPosition.y = Mathf.Clamp(newPosition.y, strikerMinY, strikerMaxY);

            // Aplicar la nueva posición al striker
            strikerRb.MovePosition(newPosition);
        }
        else if (redTeam == true)
        {
            // Movimiento del delantero en los dos ejes (Y = vertical, X = horizontal)
            Vector2 strikerMovement = new Vector2(horizontalInput, verticalInput) * strikerSpeed;
            Vector2 newPosition = strikerRb.position + strikerMovement * Time.deltaTime;

            // Limitar el movimiento del striker dentro del área de juego
            newPosition.x = Mathf.Clamp(newPosition.x, strikerMinX, strikerMaxX);
            newPosition.y = Mathf.Clamp(newPosition.y, strikerMinY, strikerMaxY);

            // Aplicar la nueva posición al striker
            strikerRb.MovePosition(newPosition);
        }

    }

    private void MoveGoalkeeper()
    {
        if (blueTeam == true)
        {
            // El portero ahora solo se moverá en el eje Y
            float newGoalkeeperY = goalkeeperRb.position.y + verticalInput * goalkeeperSpeed * Time.deltaTime;

            // Limitar el movimiento del portero dentro del rango permitido en el eje Y
            newGoalkeeperY = Mathf.Clamp(newGoalkeeperY, goalkeeperMinY, goalkeeperMaxY);

            // Aplicar el nuevo movimiento solo en el eje Y
            goalkeeperRb.MovePosition(new Vector2(goalkeeperRb.position.x, newGoalkeeperY));
        }

        else if (redTeam == true)
        {
            // El portero ahora solo se moverá en el eje Y
            float newGoalkeeperY = goalkeeperRb.position.y + verticalInput * goalkeeperSpeed * Time.deltaTime;

            // Limitar el movimiento del portero dentro del rango permitido en el eje Y
            newGoalkeeperY = Mathf.Clamp(newGoalkeeperY, goalkeeperMinY, goalkeeperMaxY);

            // Aplicar el nuevo movimiento solo en el eje Y
            goalkeeperRb.MovePosition(new Vector2(goalkeeperRb.position.x, newGoalkeeperY));
        }
    }

    private void RotateStriker()
    {
        // Si el joystick está en uso, calcular el ángulo de rotación
        if (joystick.Direction.magnitude > 0.1f)
        {
            // Calcular el ángulo con Atan2 usando el joystick
            float angle = Mathf.Atan2(verticalInput, horizontalInput) * Mathf.Rad2Deg;

            // Aplicar la rotación al delantero para que apunte en la dirección del joystick
            strikerRb.rotation = angle;
        }
    }
   
}
