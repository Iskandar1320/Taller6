using System.Drawing;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
 

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

    #region SerializeField

    [SerializeField]
    GameObject panelInicial;

    [Header("Elementos a invertir")]
    [SerializeField]
    private TextMeshProUGUI textmeshpro;
    [SerializeField]
    private Image colorpanel;
    [SerializeField]
    GameObject holder;
    [SerializeField]
    GameObject flotaRoja;
    [SerializeField]
    GameObject flotaAzul;
    [SerializeField]
    GameObject fondoTextoPosicionar;
    [SerializeField]
    GameObject textoPosicionar;
    [SerializeField]
    GameObject vidas;
    [SerializeField]
    GameObject exit;
    #endregion

    void Start()
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
        if (_setupComplete && _player1Turn)
        {
            //Empieza el juego

            Debug.Log("setteo completo");


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
            print("entro if");
        }
        else
        {
            print("no entro if");

            Debug.Log("Fin de la colocación de barcos. Cambiar turno.");
            // _setupComplete = true; // Marcar que la configuración está completa
            _player1Turn = !_player1Turn; // Cambiar turno al jugador 2
            _shipIndex = 0; // Reiniciar el índice del barco para el nuevo jugador
            _shipScript = _player1Turn ? ships[_shipIndex].GetComponent<ShipScript>() : ships2[_shipIndex].GetComponent<ShipScript>();

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
        if (cuenta<1)
        {

        panelInicial.SetActive(true);
        textmeshpro.text = "Pasale el cel a tu amigo y no mirés ome";
        colorpanel.color = new Color32(28, 39, 161, 255);
        holder.GetComponent<Transform>().transform.position += new Vector3(-2.06f, 0.0086f, 0f);
        holder.GetComponent<SpriteRenderer>().color = new Color32(87, 136, 173, 255);
        flotaAzul.SetActive(true);
        flotaRoja.SetActive(false);
        fondoTextoPosicionar.GetComponent<Transform>().position += new Vector3(-10.06f, -0.0196f, 0f);
        fondoTextoPosicionar.GetComponent<SpriteRenderer>().color = new Color32(164, 77, 64, 255);

        textoPosicionar.GetComponent<RectTransform>().eulerAngles = new Vector3(0f, 0f, 180f);
        textoPosicionar.GetComponent<RectTransform>().anchoredPosition = new Vector3(0f, -469f, 0f);
        cuenta++;
        }
        else if (cuenta == 1)
        {
            _setupComplete = true;
            _player1Turn = true;
        }


        if (_setupComplete && _player1Turn)
        {
            foreach (SpriteRenderer sr in flotaAzul.GetComponentsInChildren<SpriteRenderer>())
            {
                sr.enabled = false;
            }
            Debug.Log("apago azul");
            foreach (SpriteRenderer sr in flotaRoja.GetComponentsInChildren<SpriteRenderer>())
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
