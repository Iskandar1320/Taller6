using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sumo : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 45.0f;
    [SerializeField] private float _movementSpeed = 1.0f;
    [SerializeField] private GameObject _directionReference1;
    [SerializeField] private GameObject _directionReference2;
    [SerializeField] private Transform _p1;
    [SerializeField] private Transform _p2;
    [SerializeField] private Collider2D _manos1;
    [SerializeField] private Collider2D _manos2;
    [SerializeField] private Collider2D _gameZone;
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private GameObject panel;
    [SerializeField] private Button btn;

    private bool _isTackle1 = false;
    private bool _isTackle2 = false;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            // Detectar si el toque es sobre un UI
            if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                Debug.Log("Boton");
                
                return; // Si es sobre UI, no ejecutar el resto de la lógica
            }

            if (touch.position.y < Screen.height / 2)
            {
                _isTackle1 = true;
            }
            else
            {
                _isTackle2 = true;
            }
        }
        else
        {
            _isTackle1 = Input.GetKey(KeyCode.Q);
            _isTackle2 = Input.GetKey(KeyCode.E);
        }

        CheckPlayerInsideZone(_p1, 1);
        CheckPlayerInsideZone(_p2, 2);
    }

    private void FixedUpdate()
    {
        if (_isTackle1)
        {
            Tackle(_p1, _directionReference1.transform.up);
        }
        else
        {
            Rotate(_p1);
        }

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
        playerTransform.Rotate(0, 0, _rotationSpeed * Time.deltaTime);
    }

    private void Tackle(Transform playerTransform, Vector3 direction)
    {
        playerTransform.Translate(direction * _movementSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == _p1)
        {
            PlayerLost(1);
        }
        else if (other.transform == _p2)
        {
            PlayerLost(2);
        }
    }

    private void CheckPlayerInsideZone(Transform player, int playerNumber)
    {
        Vector2 playerPosition = player.position;

        if (!_gameZone.OverlapPoint(playerPosition))
        {
            PlayerLost(playerNumber);
        }
    }

    private void PlayerLost(int playerNumber)
    {
        _textMeshPro.text = playerNumber > 1 ? "Player 1 Wins" : "Player 2 Wins";
        panel.SetActive(true);
    }
}
