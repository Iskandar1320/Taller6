using UnityEngine;

public class Sumo : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 45.0f; // Velocidad de rotación
    [SerializeField] private float _movementSpeed = 1.0f; // Velocidad de movimiento
    [SerializeField] private GameObject _directionReference1; // Objeto de referencia para la dirección del jugador 1
    [SerializeField] private GameObject _directionReference2; // Objeto de referencia para la dirección del jugador 2
    [SerializeField] private Transform _p1; // Transform del Jugador 1
    [SerializeField] private Transform _p2; // Transform del Jugador 2

    private bool _isTackle1 = false; // Estado de movimiento para Jugador 1
    private bool _isTackle2 = false; // Estado de movimiento para Jugador 2

    private void Start()
    {
    }

    void Update()
    {
        // Jugador 1 controlado por el botón sur del gamepad
        if (Input.touchCount>(0))
        {
            _isTackle1 = true;
        }
        else
        {
            _isTackle1 = false;
        }

        // Jugador 2 controlado por el botón oeste del gamepad
        if (Input.touchCount > (0))
        {
            _isTackle2 = true;
        }
        else
        {
            _isTackle2 = false;
        }
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
}
