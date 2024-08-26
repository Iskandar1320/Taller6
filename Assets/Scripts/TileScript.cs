using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileScript : MonoBehaviour, IDropHandler
{
    GameManager gameManager;

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
        if (Input.GetMouseButtonDown(0)) // Detecta el clic izquierdo del mouse
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           
            RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Tile clickeada: " + hit.collider.name);
                gameManager.TileClicked(hit.collider.gameObject);
                // Aquí puedes manejar lo que suceda cuando se haga clic en una tile.
            }
        }
    }
}
