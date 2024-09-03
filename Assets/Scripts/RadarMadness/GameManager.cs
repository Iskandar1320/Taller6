using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject panelInicial;

    [Header("Pool ships")]
    public GameObject[] ships;
    public GameObject[] ships2;

    bool _setupComplete = false;
    private int _shipIndex = 0;
    private ShipScript _shipScript;
    int cuenta = 0;


    [Header("Turno debug")]
    public bool _player1Turn = true;
    public GameObject readybttn;
    public GameObject rotatebttn;

    [Header("Elementos a invertir")]
    [SerializeField]
    private TextMeshProUGUI textmeshpro;
    [SerializeField]
    private Image colorpanel;
    [SerializeField]
    Transform holderflota;
    [SerializeField]
    SpriteRenderer holdercolor;
    [SerializeField]
    GameObject flotaRoja;
    [SerializeField]
    GameObject flotaAzul;
    [SerializeField]
    GameObject fondoTexto;
    [SerializeField]
    SpriteRenderer fondosprite;
    [SerializeField]
    GameObject textoPosi;

    void Start()
    {
        panelInicial.SetActive(true);
        _shipScript = ships[_shipIndex].GetComponent<ShipScript>();
    }

    public void OnDisablePanel()
    {
        panelInicial.SetActive(false);
    }
    public void TileClicked(GameObject tile)
    {
        if (_setupComplete && _player1Turn)
        {
            //Empieza el juego
        }
        else if (!_setupComplete)
        {

            PlaceShip(tile);
            _shipScript.SetClickedTile(tile);

        }
    }
    public void PlaceShip(GameObject tile)
    {
        if (!panelInicial.activeSelf)
        {
            if (_player1Turn)
            {
                _shipScript = ships[_shipIndex].GetComponent<ShipScript>();
                _shipScript.ClearTileList();
                Vector3 newVec = _shipScript.GetOffsetVec(tile.transform.position);
                ships[_shipIndex].transform.localPosition = newVec;

                Transform activeTransform = ships[_shipIndex].transform;
                Vector3 newButtonPosition = activeTransform.position + new Vector3(1f, 0f, 0f); // Ajusta este Vector3 según donde quieras que se mueva el botón
                readybttn.transform.position = newButtonPosition;
                readybttn.SetActive(true);

                Vector3 newButtonPosition2 = activeTransform.position + new Vector3(-1f, 0f, 0f); // Ajusta este Vector3 según donde quieras que se mueva el botón
                rotatebttn.transform.position = newButtonPosition2;
                rotatebttn.SetActive(true);
                Debug.Log("El Transform del barco activo es: " + activeTransform.name);

            }

            if (!_player1Turn)
            {
                _shipScript = ships2[_shipIndex].GetComponent<ShipScript>();
                _shipScript.ClearTileList();
                Vector3 newVec = _shipScript.GetOffsetVec(tile.transform.position);
                ships2[_shipIndex].transform.localPosition = newVec;

                Transform activeTransform = ships2[_shipIndex].transform;
                Vector3 newButtonPosition = activeTransform.position + new Vector3(1f, 0f, 0f); // Ajusta este Vector3 según donde quieras que se mueva el botón
                readybttn.transform.position = newButtonPosition;
                readybttn.SetActive(true);

                Vector3 newButtonPosition2 = activeTransform.position + new Vector3(-1f, 0f, 0f); // Ajusta este Vector3 según donde quieras que se mueva el botón
                rotatebttn.transform.position = newButtonPosition2;
                rotatebttn.SetActive(true);
                Debug.Log("El Transform del barco activo es: " + activeTransform.name);
            }
        }


       
    }
    public void NextShipCliked()
    {
        if (_shipIndex <= ships.Length - 2)
        {
            _shipIndex++;
            _shipScript = ships[_shipIndex].GetComponent<ShipScript>();

        }
        else
        {
            Debug.Log("Fin de la colocación de barcos. Cambiar turno.");
           // _setupComplete = true; // Marcar que la configuración está completa
            _player1Turn = !_player1Turn; // Cambiar turno al jugador 2
            _shipIndex = 0; // Reiniciar el índice del barco para el nuevo jugador
            _shipScript = _player1Turn ? ships[_shipIndex].GetComponent<ShipScript>() : ships2[_shipIndex].GetComponent<ShipScript>();

            ChangeSites();

        }
    }
    public void RotateClicked()
    {
        _shipScript.RotateShip();
    }
    private void ChangeSites()
    {
        if (cuenta<1)
        {

        panelInicial.SetActive(true);
        textmeshpro.text = "Pasale el cel a tu amigo y no mirés ome";
        colorpanel.color = new Color32(28, 39, 161, 255);
        holderflota.transform.position += new Vector3(-2.06f, 0.0086f, 0f);
        holdercolor.color = new Color32(87, 136, 173, 255);
        flotaAzul.SetActive(true);
        flotaRoja.SetActive(false);
        fondoTexto.transform.position += new Vector3(-10.06f, -0.0196f, 0f);
        fondosprite.color = new Color32(164, 77, 64, 255);

        textoPosi.GetComponent<RectTransform>().eulerAngles = new Vector3(0f, 0f, 180f);
        textoPosi.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, -469f, 0f);
        cuenta++;
        }
        else
        {
        _setupComplete = true;
        _player1Turn = true;
        }
    }

}
