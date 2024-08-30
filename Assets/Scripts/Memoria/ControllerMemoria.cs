using System.Collections;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class ControllerMemoria : MonoBehaviour
{
    private const int columns = 4;
    private const int rows = 4;

    private const float Xspace = .8f;
    private const float Yspace = -2f;

    [SerializeField]
    private MemoriaImagen _startObject;
    [SerializeField]
    private Sprite[] _images;

    private int currentPlayer = 1; // 1 para Jugador 1, 2 para Jugador 2
    private int scorePlayer1 = 0;
    private int scorePlayer2 = 0;

    [SerializeField] private TextMeshProUGUI scoreTextPlayer1;
    [SerializeField] private TextMeshProUGUI scoreTextPlayer2;
    [SerializeField] private TextMeshProUGUI turnText;
    [SerializeField] private TextMeshProUGUI turnText2;
    [SerializeField] private SpriteRenderer _unknownRend; // Objeto principal con el color deseado
    [SerializeField] private GameObject panelwin; // Objeto principal con el color deseado
    [SerializeField] private Image colorpanel;
    [SerializeField] private TextMeshProUGUI _textMeshPro;

    private void Update()
    {
        Debug.Log(scorePlayer1 + " " + scorePlayer2);
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

    private void Start()
    {
        int[] locations = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
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

                // Asigna el color del main a cada carta clonada
                SpriteRenderer spriteRenderer = gameImage.GetComponent<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    if (currentPlayer == 1)
                    {
                        _unknownRend.color = new Color32(161, 28, 28, 255); 

                    }
                    else
                    {
                        _unknownRend.color = new Color32(28, 39, 161, 255);


                    }
                }
                else
                {
                    Debug.LogError("SpriteRenderer no encontrado en " + gameImage.name);
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
    

    private MemoriaImagen firstOpen;
    private MemoriaImagen secondOpen;

    public bool canOpen
    {
        get { return secondOpen == null; }
    }

    public void ImageOpened(MemoriaImagen startObject)
    {
        Winner();
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
                scoreTextPlayer1.text = "" + scorePlayer1;
            }
            else
            {
                scorePlayer2++;
                scoreTextPlayer2.text = "" + scorePlayer2;
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
            turnText.enabled = true;
            turnText.text = "Player " + currentPlayer;
            turnText.color = Color.red;
          //  main.color = Color.red;
            turnText2.enabled = false;
            
        }
        else
        {
            turnText2.enabled = true;
            turnText2.text = "Player " + currentPlayer;
            turnText2.color = Color.blue;
           // main.color = Color.blue;
            turnText.enabled = false;

        }
    }
    private void Winner()
    {
        // Verifica si ambos jugadores han acumulado todas las cartas (en este caso, 8)
        if (scorePlayer1 + scorePlayer2 == 8)
        {
            if (scorePlayer1 > scorePlayer2)
            {
                // Jugador 1 gana
                _textMeshPro.text = "Player 1 Wins";
                colorpanel.color = new Color32(161, 28, 28, 233);
                panelwin.SetActive(true);
            }
            else if (scorePlayer2 > scorePlayer1)
            {
                // Jugador 2 gana
                _textMeshPro.text = "Player 2 Wins";
                colorpanel.color = new Color32(28, 39, 161, 233);
                panelwin.SetActive(true);
            }
            else if (scorePlayer1 == scorePlayer2)
            {
                // Empate
                _textMeshPro.text = "Empate";
                colorpanel.color = new Color32(117, 5, 226, 255);
                panelwin.SetActive(true);
            }
        }
    }

}
