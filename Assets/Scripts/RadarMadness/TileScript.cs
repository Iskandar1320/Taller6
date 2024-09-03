using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour, IDropHandler
{
    GameManager gameManager;
    Ray ray;
    RaycastHit hit;

    private bool missileHit = false;
    Color32[] hitColor = new Color32[2];

    public LayerMask interactableLayer;
    public LayerMask groundLayer;

    public void OnDrop(PointerEventData eventData)
    {
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition= GetComponent<RectTransform>().anchoredPosition;
        }
    }

    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100f, interactableLayer))
            {
                // Si el raycast golpea un objeto con la capa interactuable, procesa el clic ahí
                hit.collider.gameObject.GetComponent<FakeBttn>().OnMouseUpAsButton();
                Debug.Log("Ay miguel");
                return; // Sale de la función para no procesar el clic en el terreno
            }
            else if (Physics.Raycast(ray, out hit, 100f, groundLayer)) // Detecta el clic izquierdo del mouse
            {
                if (Input.GetMouseButton(0) && hit.collider.gameObject.name == gameObject.name)
                {
                    if (missileHit == false)
                    {
                        gameManager.TileClicked(hit.collider.gameObject);

                    }
                }
            }
        }
        
        Debug.DrawRay(ray.origin, ray.direction*30f,Color.green);
    }
}
