using System;
using TMPro.Examples;
using UnityEngine;
using TMPro;

public class Sumo : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 45.0f; // Velocidad de rotación
    [SerializeField] private float _movementSpeed = 1.0f; // Velocidad de movimiento
    [SerializeField] private GameObject _directionReference1; // Objeto de referencia para la dirección del jugador 1
    [SerializeField] private GameObject _directionReference2; // Objeto de referencia para la dirección del jugador 2
    [SerializeField] private Transform _p1; // Transform del Jugador 1
    [SerializeField] private Transform _p2; // Transform del Jugador 2
    [SerializeField] private Collider2D _manos1;
    [SerializeField] private Collider2D _manos2;
    [SerializeField] private Collider2D _gameZone; // Zona de juego
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private GameObject panel;



    private bool _isTackle1 = false; // Estado de movimiento para Jugador 1
    private bool _isTackle2 = false; // Estado de movimiento para Jugador 2

    private void Start()
    {
    }

    void Update()
    {
        if (!_isTackle1) 
        {
            _manos1.enabled = false;
        }
        else
        {
            _manos1.enabled=true;
        }

        if (!_isTackle2)
        {
            _manos2.enabled = false;
        }
        else
        {
            _manos2.enabled=true;
        }

        // Jugador 1 controlado por el botón sur del gamepad
        if (Input.touchCount>(0) || Input.GetKey(KeyCode.Q))
        {
            _isTackle1 = true;
        }
        else
        {
            _isTackle1 = false;
        }

        // Jugador 2 controlado por el botón oeste del gamepad
        if (Input.touchCount > (0) || Input.GetKey(KeyCode.E))
        {
            _isTackle2 = true;
        }
        else
        {
            _isTackle2 = false;
        }
        // Comprobar si los jugadores están dentro de la zona
        CheckPlayerInsideZone(_p1, 1);
        CheckPlayerInsideZone(_p2, 2);
    }


    private void FixedUpdate()
    {
        // Mover y rotar Jugador 1
        if (_isTackle1)
        {
            Tackle(_p1, _directionReference1.transform.up);
        }
        else
        {
            Rotate(_p1);
        }

        // Mover y rotar Jugador 2
        if (_isTackle2)
        {
            Tackle(_p2, _directionReference2.transform.up);
        }
        else
        {
            Rotate(_p2);
        }
    }

    private void Rotate(Transform playerTransform)
    {
        // Rotar el jugador
        playerTransform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
    }

    private void Tackle(Transform playerTransform, Vector3 direction)
    {
        // Mover el jugador en la dirección del objeto de referencia
        playerTransform.Translate(direction * _movementSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == _p1)
        {
            Debug.Log("Jugador 1 ha salido de la zona.");
            PlayerLost(1);
        }
        else if (other.transform == _p2)
        {
            Debug.Log("Jugador 2 ha salido de la zona.");
            PlayerLost(2);
        }
    }
    private void CheckPlayerInsideZone(Transform player, int playerNumber)
    {
        // Convertir la posición del jugador en una posición 2D y comprobar si está dentro de la zona
        Vector2 playerPosition = player.position;

        if (_gameZone.OverlapPoint(playerPosition))
        {
            Debug.Log("Jugador " + playerNumber + " está dentro de la zona.");
        }
        else
        {
            Debug.Log("Jugador " + playerNumber + " está fuera de la zona.");
            PlayerLost(playerNumber);
        }
    }

    private void PlayerLost(int playerNumber)
    {
        
        Debug.Log("Jugador " + playerNumber + " ha perdido.");
        // Aquí puedes añadir la lógica para terminar el juego, reiniciar, etc.
        _textMeshPro.text = playerNumber > 1 ? "Player 1 Wins" : "Player 2 Wins";
        panel.SetActive(true);
    }
}
