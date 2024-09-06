using UnityEngine;
using UnityEngine.EventSystems;

namespace RadarMadness
{
    public class TileScript : MonoBehaviour, IDropHandler
    {
        private GameManager _gameManager;
        private Ray _ray;
        private RaycastHit _hit;
    
        private bool _missileHit;
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

        private void Start()
        {
            _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }

        // Update is called once per frame
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Camera.main != null) _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(_ray, out _hit, 100f, interactableLayer))
                {
                    // Si el raycast golpea un objeto con la capa interactuable, procesa el clic ah�
                    _hit.collider.gameObject.GetComponent<FakeBttn>().OnMouseUpAsButton();
                    Debug.Log("Ay miguel");
                    return; // Sale de la funci�n para no procesar el clic en el terreno
                }
                else if (Physics.Raycast(_ray, out _hit, 100f, groundLayer)) // Detecta el clic izquierdo del mouse
                {
                    if (Input.GetMouseButton(0) && _hit.collider.gameObject.name == gameObject.name)
                    {
                        if (_missileHit == false)
                        {
                            _gameManager.TileClicked(_hit.collider.gameObject);

                        }
                    }
                }
            }
        
            Debug.DrawRay(_ray.origin, _ray.direction*30f,Color.green);
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Missile"))
            {
                _missileHit = true;
            }
        }
    }

}
