using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace RadarMadness
{
    public class GameManager : MonoBehaviour
  {

    [Header("Pool ships")]
    public GameObject[] ships;
    public GameObject[] ships2;

    private bool _setupComplete = false;
    private int _shipIndex = 0;
    private ShipScript _shipScript;
    private int _cuenta = 0;


    [FormerlySerializedAs("_player1Turn")] [Header("Turno debug")]
    public bool player1Turn = true;
    public GameObject readybttn;
    public GameObject rotatebttn;

    #region SerializeField

    [SerializeField]
    private GameObject panelInicial;

    [Header("Elementos a invertir")]
    [SerializeField]
    private TextMeshProUGUI textmeshpro;
    [SerializeField]
    private Image colorpanel;
    [SerializeField]
    private GameObject holder;
    [SerializeField]
    private GameObject flotaRoja;
    [SerializeField]
    private GameObject flotaAzul;
    [SerializeField]
    private GameObject fondoTextoPosicionar;
    [SerializeField]
    private GameObject textoPosicionar;
    [SerializeField]
    private GameObject vidas;
    [SerializeField]
    private GameObject exit;
    #endregion

    private void Start()
    {
        panelInicial.SetActive(true);
        _shipScript = ships[_shipIndex].GetComponent<ShipScript>();
    }
    private void Update()
    {
        Debug.Log(_setupComplete);
    }
    public void OnDisablePanel()
    {
        panelInicial.SetActive(false);
    }
    public void TileClicked(GameObject tile)
    {
        switch (_setupComplete)
        {
            case true when player1Turn:
                //Empieza el juego

                Debug.Log("setteo completo");
                break;
            case false:
                PlaceShip(tile);
                _shipScript.SetClickedTile(tile);
                break;
        }
    }
    private void PlaceShip(GameObject tile)
    {
        if (panelInicial.activeSelf) return;
        if (player1Turn)
        {
            _shipScript = ships[_shipIndex].GetComponent<ShipScript>();
            _shipScript.ClearTileList();
            var newVec = _shipScript.GetOffsetVec(tile.transform.position);
            ships[_shipIndex].transform.localPosition = newVec;

            var activeTransform = ships[_shipIndex].transform;
            var newButtonPosition = activeTransform.position + new Vector3(1f, 0f, 0f); // Ajusta este Vector3 seg�n donde quieras que se mueva el bot�n
            readybttn.transform.position = newButtonPosition;
            readybttn.SetActive(true);

            var newButtonPosition2 = activeTransform.position + new Vector3(-1f, 0f, 0f); // Ajusta este Vector3 seg�n donde quieras que se mueva el bot�n
            rotatebttn.transform.position = newButtonPosition2;
            rotatebttn.SetActive(true);
            Debug.Log("El Transform del barco activo es: " + activeTransform.name);

        }

        if (player1Turn) return;
        {
            _shipScript = ships2[_shipIndex].GetComponent<ShipScript>();
            _shipScript.ClearTileList();
            var newVec = _shipScript.GetOffsetVec(tile.transform.position);
            ships2[_shipIndex].transform.localPosition = newVec;

            var activeTransform = ships2[_shipIndex].transform;
            var newButtonPosition = activeTransform.position + new Vector3(1f, 0f, 0f); // Ajusta este Vector3 seg�n donde quieras que se mueva el bot�n
            readybttn.transform.position = newButtonPosition;
            readybttn.SetActive(true);

            var newButtonPosition2 = activeTransform.position + new Vector3(-1f, 0f, 0f); // Ajusta este Vector3 seg�n donde quieras que se mueva el bot�n
            rotatebttn.transform.position = newButtonPosition2;
            rotatebttn.SetActive(true);
            Debug.Log("El Transform del barco activo es: " + activeTransform.name);
        }



    }
    public void NextShipClicked()
    {
        if (_shipIndex <= ships.Length - 2)
        {
            _shipIndex++;
            _shipScript = ships[_shipIndex].GetComponent<ShipScript>();
            print("entro if");
        }
        else
        {
            print("no entro if");

            Debug.Log("Fin de la colocaci�n de barcos. Cambiar turno.");
            // _setupComplete = true; // Marcar que la configuraci�n est� completa
            player1Turn = !player1Turn; // Cambiar turno al jugador 2
            _shipIndex = 0; // Reiniciar el �ndice del barco para el nuevo jugador
            _shipScript = player1Turn ? ships[_shipIndex].GetComponent<ShipScript>() : ships2[_shipIndex].GetComponent<ShipScript>();

            ChangeSites();
        }
        if (!_shipScript.OngameBoard())
        {
                print("a ya");

        }
    }
    public void RotateClicked()
    {
        _shipScript.RotateShip();
    }
    private void ChangeSites()
    {
        switch (_cuenta)
        {
            case < 1:
                panelInicial.SetActive(true);
                textmeshpro.text = "Pasale el cel a tu amigo y no mir�s ome";
                colorpanel.color = new Color32(28, 39, 161, 255);
                holder.GetComponent<Transform>().transform.position += new Vector3(-2.06f, 0.0086f, 0f);
                holder.GetComponent<SpriteRenderer>().color = new Color32(87, 136, 173, 255);
                flotaAzul.SetActive(true);
                flotaRoja.SetActive(false);
                fondoTextoPosicionar.GetComponent<Transform>().position += new Vector3(-10.06f, -0.0196f, 0f);
                fondoTextoPosicionar.GetComponent<SpriteRenderer>().color = new Color32(164, 77, 64, 255);

                textoPosicionar.GetComponent<RectTransform>().eulerAngles = new Vector3(0f, 0f, 180f);
                textoPosicionar.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, -469f, 0f);
                _cuenta++;
                break;
            case 1:
                _setupComplete = true;
                player1Turn = true;
                break;
        }


        if (!_setupComplete || !player1Turn) return;
        foreach (var sr in flotaAzul.GetComponentsInChildren<SpriteRenderer>())
        {
            sr.enabled = false;
        }
        Debug.Log("apago azul");
        foreach (var sr in flotaRoja.GetComponentsInChildren<SpriteRenderer>())
        {
            sr.enabled = false;
        }
        textoPosicionar.SetActive(false);
        fondoTextoPosicionar.SetActive(false);
        vidas.SetActive(true);
        holder.SetActive(false);
        exit.SetActive(true);
    }
  }
}
