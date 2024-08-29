using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;
using Unity.VisualScripting;
using System.Collections;

public class Sumo : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 45.0f;
    [SerializeField] private float _movementSpeed = 1.0f;
    [SerializeField] private GameObject _directionReference1;
    [SerializeField] private GameObject _directionReference2;
    [SerializeField] private Transform _p1;
    [SerializeField] private Transform _p2;
    [SerializeField] private Collider2D _gameZone;
    [SerializeField] private TextMeshProUGUI _textMeshPro;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject mano1;
    [SerializeField] private GameObject mano2;
    [SerializeField] private Image colorpanel;
    [SerializeField] private TextMeshProUGUI _puntosAzul;
    [SerializeField] private TextMeshProUGUI _puntosRojo;

    private Vector3 _p1InitialPosition;
    private Vector3 _p2InitialPosition;
    private bool _isTackle1 = false;
    private bool _isTackle2 = false;
    private int _roundsBlue = 0;
    private int _roundsRed = 0;
    private bool _canAddPoints = true;
    private bool _canMove = false; // Nueva variable para controlar el movimiento de los jugadores

    [SerializeField] private GameObject _panelTime;
    [SerializeField] private TextMeshProUGUI _timerText;
    private float _timeNow;
    private float _referenceTime = 4f;
    private bool _runningTime;

    private void Start()
    {
        _p1InitialPosition = _p1.position;
        _p2InitialPosition = _p2.position;

        // Iniciar la cuenta regresiva al inicio del juego
        StartCoroutine(IniciaTimerCoroutine());
    }

    void Update()
    {
        if (_runningTime)
        {
            _timeNow -= Time.deltaTime;
            _timerText.text = Mathf.FloorToInt(_timeNow).ToString();
            if (_timeNow <= 1f)
            {
                _timeNow = 1f;
                _timerText.text = Mathf.FloorToInt(_timeNow).ToString();
                _runningTime = false;

                TimerExpira();
            }
        }

        Debug.Log("blue: " + _roundsBlue + " red: " + _roundsRed);
        Touching();
        CheckPlayerInsideZone(_p1, 1);
        CheckPlayerInsideZone(_p2, 2);
    }

    private void Touching()
    {
        if (_canMove == true) // Verificar si los jugadores pueden moverse
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    return;
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
        }
    }

    private void FixedUpdate()
    {
        Moving();
    }

    private void Moving()
    {
        if (_canMove) // Verificar si los jugadores pueden moverse
        {
            if (_isTackle1)
            {
                mano1.SetActive(true);
                Tackle(_p1, _directionReference1.transform.up);
            }
            else
            {
                mano1.SetActive(false);
                Rotate(_p1);
            }

            if (_isTackle2)
            {
                mano2.SetActive(true);
                Tackle(_p2, _directionReference2.transform.up);
            }
            else
            {
                mano2.SetActive(false);
                Rotate(_p2);
            }
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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_canAddPoints)
        {
            if (other.transform == _p1)
            {
                StartCoroutine(HandlePlayerLost(1));
            }
            else if (other.transform == _p2)
            {
                StartCoroutine(HandlePlayerLost(2));
            }
        }
    }

    private void CheckPlayerInsideZone(Transform player, int playerNumber)
    {
        Vector2 playerPosition = player.position;

        if (!_gameZone.OverlapPoint(playerPosition) && _canAddPoints)
        {
            StartCoroutine(HandlePlayerLost(playerNumber));
        }
    }

    private IEnumerator HandlePlayerLost(int playerNumber)
    {
        _canAddPoints = false;

        _canMove = false;

        if (playerNumber == 1)
        {
            _roundsBlue++;
        }
        else if (playerNumber == 2)
        {
            _roundsRed++;
        }

        UpdateScoreUI();
        yield return new WaitForSeconds(1.5f);

        ResetPlayerPositions();

        if (_roundsRed == 3)
        {
            _textMeshPro.text = "Player 1 Wins";
            colorpanel.color = new Color32(161, 28, 28, 233);
            panel.SetActive(true);
            StartCoroutine(RestartGame());
        }
        else if (_roundsBlue == 3)
        {
            _textMeshPro.text = "Player 2 Wins";
            colorpanel.color = new Color32(28, 39, 161, 233);
            panel.SetActive(true);
            StartCoroutine(RestartGame());
        }
        else
        {
            _canAddPoints = true;
        }
    }

    private void ResetPlayerPositions()
    {
        _p1.position = _p1InitialPosition;
        _p2.position = _p2InitialPosition;

        _p1.rotation = Quaternion.Euler(0, 0, 90);
        _p2.rotation = Quaternion.Euler(0, 0, 90);

        mano1.SetActive(false);
        mano2.SetActive(false);

        ResetTimer();
        _canMove = false; // Desactivar movimiento hastsa que termine la cuenta regresiva
        if (_roundsBlue < 3 && _roundsRed < 3)
        {
        StartCoroutine(IniciaTimerCoroutine());
        }
    }

    private void UpdateScoreUI()
    {
        _puntosAzul.text = _roundsBlue.ToString();
        _puntosRojo.text = _roundsRed.ToString();
    }

    private IEnumerator RestartGame()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene(0);
    }

    private IEnumerator CuentaAtras()
    {
        yield return new WaitForSeconds(0f);
        _panelTime.SetActive(false);
    }

    private IEnumerator IniciaTimerCoroutine()
    {
        ResetTimer();
        _panelTime.SetActive(true);
        _runningTime = true;

        // Espera a que la cuenta regresiva termine
        while (_runningTime)
        {
            yield return null;
        }

        // Habilitar el movimiento después de la cuenta regresiva
        _canMove = true;
        _panelTime.SetActive(false);
    }

    private void ResetTimer()
    {
        _timeNow = _referenceTime;
        _timerText.text = Mathf.FloorToInt(_timeNow).ToString();
        _runningTime = false;
    }

    private void TimerExpira()
    {
        _panelTime.SetActive(false);
    }
}
