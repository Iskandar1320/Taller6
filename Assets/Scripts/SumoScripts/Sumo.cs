using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SumoScripts
{
    public class Sumo : MonoBehaviour
    {

        #region SerializedFields
        
        [SerializeField] private float rotationSpeed = 45.0f;
        [SerializeField] private float movementSpeed = 1.0f;
        [SerializeField] private GameObject directionReference1;
        [SerializeField] private GameObject directionReference2;
        [SerializeField] private Transform p1;
        [SerializeField] private Transform p2;
        [SerializeField] private Collider2D gameZone;
        [SerializeField] private TextMeshProUGUI textMeshPro;
        [SerializeField] private GameObject panel;
        [SerializeField] private GameObject mano1;
        [SerializeField] private GameObject mano2;
        [SerializeField] private Image colorpanel;
        [SerializeField] private TextMeshProUGUI puntosAzul;
        [SerializeField] private TextMeshProUGUI puntosRojo;
        [SerializeField] private Animator animator;
        #endregion

        private Vector3 _p1InitialPosition;
        private Vector3 _p2InitialPosition;
        private bool _isTackle1;
        private bool _isTackle2;
        private int _roundsBlue =2;
        private int _roundsRed;
        private bool _canAddPoints = true;
        private bool _canMove; // Nueva variable para controlar el movimiento de los jugadores
        private SceneTransitions _sceneTransitions;

        [SerializeField] private GameObject panelTime;
        [SerializeField] private TextMeshProUGUI timerText;
        private float _timeNow;
        private float _referenceTime = 4f;
        private bool _runningTime;

        private void Start()
        {
            
            _sceneTransitions = FindObjectOfType<SceneTransitions>();
            
            _p1InitialPosition = p1.position;
            _p2InitialPosition = p2.position;
            StartCoroutine(StartAnimCoroutine());
            // Iniciar la cuenta regresiva al inicio del juego
        }

        private void Update()
        {
            if (_runningTime)
            {
                _timeNow -= Time.deltaTime;
                timerText.text = Mathf.FloorToInt(_timeNow).ToString();
                if (_timeNow <= 1f)
                {
                    _timeNow = 1f;
                    timerText.text = Mathf.FloorToInt(_timeNow).ToString();
                    _runningTime = false;

                    TimerExpire();
                }
            }

            Debug.Log("blue: " + _roundsBlue + " red: " + _roundsRed);
            Touching();
            CheckPlayerInsideZone(p1, 1);
            CheckPlayerInsideZone(p2, 2);
        }

        private void Touching()
        {
            if (!_canMove) return; // Verificar si los jugadores pueden moverse
    
            if (Input.touchCount > 0)
            {
                var touch = Input.GetTouch(0);

                // Check if the touch is over a UI element
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    return;
                }

                // Normalize the y-coordinate
                float normalizedY = touch.position.y / Screen.height;

                // Use 0.5 as the middle point in normalized space (instead of Screen.height / 2)
                if (normalizedY < 0.5f)
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

        private void FixedUpdate()
        {
            Moving();
        }

        private void Moving()
        {
            if (!_canMove) return; // Verificar si los jugadores pueden moverse
            if (_isTackle1)
            {
                mano1.SetActive(true);
                Tackle(p1, directionReference1.transform.up);
            }
            else
            {
                mano1.SetActive(false);
                Rotate(p1);
            }

            if (_isTackle2)
            {
                mano2.SetActive(true);
                Tackle(p2, directionReference2.transform.up);
            }
            else
            {
                mano2.SetActive(false);
                Rotate(p2);
            }
        }

        private void Rotate(Transform playerTransform)
        {
            playerTransform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
        }

        private void Tackle(Transform playerTransform, Vector3 direction)
        {
            playerTransform.Translate(direction * movementSpeed * Time.deltaTime, Space.World);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!_canAddPoints) return;
            if (other.transform == p1)
            {
                StartCoroutine(HandlePlayerLost(1));
            }
            else if (other.transform == p2)
            {
                StartCoroutine(HandlePlayerLost(2));
            }
        }

        private void CheckPlayerInsideZone(Transform player, int playerNumber)
        {
            Vector2 playerPosition = player.position;

            if (!gameZone.OverlapPoint(playerPosition) && _canAddPoints)
            {
                StartCoroutine(HandlePlayerLost(playerNumber));
            }
        }

        private IEnumerator HandlePlayerLost(int playerNumber)
        {
            _canAddPoints = false;

            _canMove = false;

            switch (playerNumber)
            {
                case 1:
                    _roundsBlue++;
                    break;
                case 2:
                    _roundsRed++;
                    break;
            }

            UpdateScoreUI();
            yield return new WaitForSeconds(1.5f);

            ResetPlayerPositions();

            if (_roundsRed == 3)
            {
                textMeshPro.text = "Player 1 Wins";
                colorpanel.color = new Color32(161, 28, 28, 233);
                panel.SetActive(true);
                animator.Play("EntreEscenas");
                StartCoroutine(_sceneTransitions.EndScene());
            }
            else if (_roundsBlue == 3)
            {
                textMeshPro.text = "Player 2 Wins";
                colorpanel.color = new Color32(28, 39, 161, 233);
                panel.SetActive(true);
                animator.Play("EntreEscenas");
                StartCoroutine(_sceneTransitions.EndScene());

            }
            else
            {
                _canAddPoints = true;
            }
        }

        private void ResetPlayerPositions()
        {
            p1.position = _p1InitialPosition;
            p2.position = _p2InitialPosition;

            p1.rotation = Quaternion.Euler(0, 0, 90);
            p2.rotation = Quaternion.Euler(0, 0, 90);

            mano1.SetActive(false);
            mano2.SetActive(false);

            ResetTimer();
            _canMove = false; // Desactivar movimiento hastsa que termine la cuenta regresiva
            if (_roundsBlue < 3 && _roundsRed < 3) StartCoroutine(StartTimerCoroutine()); 
        }

        private void UpdateScoreUI()
        {
            puntosAzul.text = _roundsBlue.ToString();
            puntosRojo.text = _roundsRed.ToString();
        }
        private IEnumerator StartAnimCoroutine()
        {
            animator.Play("MenuEnter");            

            yield return StartCoroutine(StartTimerCoroutine());

        }
        private IEnumerator StartTimerCoroutine()
        {
            ResetTimer();
            panelTime.SetActive(true);
            _runningTime = true;

            // Espera a que la cuenta regresiva termine
            while (_runningTime)
            {
                yield return null;
            }

            // Habilitar el movimiento despu�s de la cuenta regresiva
            _canMove = true;
            panelTime.SetActive(false);
        }

        private void ResetTimer()
        {
            _timeNow = _referenceTime;
            timerText.text = Mathf.FloorToInt(_timeNow).ToString();
            _runningTime = false;
        }

        private void TimerExpire()
        {
            panelTime.SetActive(false);
        }
     }
}
