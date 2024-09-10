using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Memoria
{
    public class ControllerMemoria : MonoBehaviour
    {
        private const int columns = 2;
        private const int rows = 6;

        private const float Xspace = 1.5f;
        private const float Yspace = -1f;

        [SerializeField]
        private MemoriaImagen _startObject;
        [SerializeField]
        private Sprite[] _images;

        private int currentPlayer = 1; // 1 para Jugador 1, 2 para Jugador 2
        private int scorePlayer1 = 0;
        private int scorePlayer2 = 0;
        private SceneTransitions _sceneTransitions;
        bool ganador;

        

        [SerializeField] private TextMeshProUGUI scoreTextPlayer1;
        [SerializeField] private TextMeshProUGUI scoreTextPlayer2;
        [SerializeField] private TextMeshProUGUI turnText;
        [SerializeField] private TextMeshProUGUI turnText2;
        [SerializeField] private GameObject panelwin; 
        [SerializeField] private Image colorpanel;
        [SerializeField] private TextMeshProUGUI _textMeshPro;
        [SerializeField] private SpriteRenderer _mesa; // Objeto principal con el color deseado
        [SerializeField] private Animator animator;

        
         private void Start()
         {
                    _sceneTransitions = FindObjectOfType<SceneTransitions>();
                    
                    int[] locations = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5 };
                    locations = Randomizer(locations);
        
                    float totalWidth = (columns - 1) * Xspace;
                    float totalHeight = (rows - 1) * Mathf.Abs(Yspace);
        
                    Vector3 startPosition = _startObject.transform.position;
                    Vector3 offset = new Vector3(-totalWidth / 2, totalHeight / 2, 0);
        
                    for (int i = 0; i < columns; i++)
                    {
                        for (int j = 0; j < rows; j++)
                        {
                            MemoriaImagen gameImage;
                            if (i == 0 && j == 0)
                            {
                                gameImage = _startObject;
                            }
                            else
                            {
                                gameImage = Instantiate(_startObject) as MemoriaImagen;
                            }
        
                            int index = j * columns + i;
                            int id = locations[index];
                            gameImage.ChangeSprite(id, _images[id]);
        
                            float positionX = Xspace * i;
                            float positionY = Yspace * j;
        
                            Vector3 targetPosition = startPosition + offset + new Vector3(positionX, positionY, 0);
                            gameImage.transform.DOMove(targetPosition, 0.5f).SetEase(Ease.OutBounce);
                        }
                    }
        
                    UpdateTurnText();
         }
        
        private void Update()
        {
            print(ganador);
            Debug.Log("suma"+scorePlayer1 + scorePlayer2);
            if (!ganador)
            {
            if (scorePlayer1 + scorePlayer2 == 6 )
            {
                ganador = true;
                Winner();
            }
                
            }
        }

        private int[] Randomizer(int[] locations)
        {
            int[] array = locations.Clone() as int[];
            for (int i = 0; i < array.Length; i++)
            {
                int newArray = array[i];
                int j = Random.Range(i, array.Length);
                array[i] = array[j];
                array[j] = newArray;
            }
            return array;
        }

       
    

        private MemoriaImagen firstOpen;
        private MemoriaImagen secondOpen;

        public bool canOpen
        {
            get { return secondOpen == null; }
        }

        public void ImageOpened(MemoriaImagen startObject)
        {
            if (firstOpen == null)
            {
                firstOpen = startObject;
            }
            else
            {
                secondOpen = startObject;
                StartCoroutine(CheckGuessed());
            }
        }

        private IEnumerator CheckGuessed()
        {
            if (firstOpen.spriteId == secondOpen.spriteId)
            {
                if (currentPlayer == 1)
                {
                    scorePlayer1++;
                    scoreTextPlayer1.text = scorePlayer1.ToString();
                }
                else
                {
                    scorePlayer2++;
                    scoreTextPlayer2.text = scorePlayer2.ToString();
                }
            }
            else
            {
                yield return new WaitForSeconds(0.5f);

                firstOpen.Close();
                secondOpen.Close();

                currentPlayer = currentPlayer == 1 ? 2 : 1;
                UpdateTurnText();
            }

            firstOpen = null;
            secondOpen = null;
        }

        private void UpdateTurnText()
        {
            if (currentPlayer == 1)
            {
                _mesa.color = new Color32(161, 28, 28, 255);

            }
            else
            {
                _mesa.color = new Color32(28, 39, 161, 255);
            }
        }
        private void Winner()
        {
            // Verifica si ambos jugadores han acumulado todas las cartas (en este caso, 8)
            if (scorePlayer1 + scorePlayer2 == 6)
            {
                print("entra a if de igual 6");
                if (scorePlayer1 > scorePlayer2)
                {
                    // Jugador 1 gana
                    _textMeshPro.text = "Player 1 Wins";
                    colorpanel.color = new Color32(161, 28, 28, 233);
                    StartCoroutine(_sceneTransitions.EndScene());
                }
                else if (scorePlayer2 > scorePlayer1)
                {
                    // Jugador 2 gana
                    _textMeshPro.text = "Player 2 Wins";
                    colorpanel.color = new Color32(28, 39, 161, 233);
                    StartCoroutine(_sceneTransitions.EndScene());
                }
                else if (scorePlayer1 == scorePlayer2)
                {
                    // Empate
                    _textMeshPro.text = "Empate";
                    colorpanel.color = new Color32(117, 5, 226, 255);
                    animator.Play("EntreEscenas");
                    StartCoroutine(_sceneTransitions.EndScene());
                }
                panelwin.SetActive(true);
                StartCoroutine(RestartGame());
            }
        }
        private IEnumerator RestartGame()
        {
            yield return new WaitForSeconds(5);
            SceneManager.LoadScene(0);
        }

    }
}
