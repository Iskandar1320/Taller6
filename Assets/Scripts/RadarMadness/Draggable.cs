using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RadarMadness
{
    public class Draggable : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private RectTransform rectTransform;
        private Vector2 initialPosition; // Variable para almacenar la posici�n inicial
        private int rotationState = 0; // 0 = sin rotar, 1 = rotado a 90 grados

        [SerializeField]
        private Canvas _canvas;
        [SerializeField]
        private CanvasGroup _canvasGroup;
        [SerializeField]
        private float Threshold = 50f;

        [Header("Limites en Fila")]
        // L�mites en el espacio de coordenadas de UI para cuando el objeto no est� rotado
        [SerializeField]
        private float minXNormal = 0f;
        [SerializeField]
        private float maxXNormal = 1000f;
        [SerializeField]
        private float minYNormal = 0f;
        [SerializeField]
        private float maxYNormal = 1000f;


        [Header("Limites en Columna")]
        // L�mites en el espacio de coordenadas de UI para cuando el objeto est� rotado
        [SerializeField]
        private float minXRotated = 0f;
        [SerializeField]
        private float maxXRotated = 1000f;
        [SerializeField]
        private float minYRotated = 0f;
        [SerializeField]
        private float maxYRotated = 1000f;

        [SerializeField]
        private List<RectTransform> uiElementsToCompare;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            initialPosition = rectTransform.anchoredPosition; // Guarda la posici�n inicial al inicio
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            Debug.Log("OnBeginDrag");
            _canvasGroup.alpha = .8f;
            _canvasGroup.blocksRaycasts = false;

            initialPosition = rectTransform.anchoredPosition; // Guarda la posici�n inicial al comenzar a arrastrar
        }

        public void OnDrag(PointerEventData eventData)
        {
            // Mueve el objeto mientras es arrastrado
            rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;

            // Limitar el movimiento dentro de los l�mites definidos seg�n el estado de rotaci�n
            Vector2 pos = rectTransform.anchoredPosition;

            if (rotationState == 0) // Sin rotar
            {
                pos.x = Mathf.Clamp(pos.x, minXNormal, maxXNormal);
                pos.y = Mathf.Clamp(pos.y, minYNormal, maxYNormal);
            }
            else if (rotationState == 1) // Rotado a 90 grados
            {
                pos.x = Mathf.Clamp(pos.x, minXRotated, maxXRotated);
                pos.y = Mathf.Clamp(pos.y, minYRotated, maxYRotated);
            }

            rectTransform.anchoredPosition = pos;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Debug.Log("OnEndDrag");

            // Comprobar si el objeto se puede soltar
            if (CanDrop())
            {
                Debug.Log("Objeto soltado correctamente.");
            }
            else
            {
                Debug.Log("No se puede soltar el objeto aqu�.");
                // Volver a la posici�n inicial si no se puede soltar
                rectTransform.anchoredPosition = initialPosition;
            }

            // Restaurar la interacci�n
            _canvasGroup.alpha = 1f;
            _canvasGroup.blocksRaycasts = true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log("OnPointerDown");

            // Verifica si el objeto est� alineado con un UIElement antes de rotar
            if (IsAlignedWithUIElement())
            {
                RotateObject();
            }
        }

        private bool IsAlignedWithUIElement()
        {
            // Comprobar si el objeto est� alineado con alg�n elemento de la lista
            foreach (RectTransform uiElement in uiElementsToCompare)
            {
                if (Vector2.Distance(rectTransform.anchoredPosition, uiElement.anchoredPosition) < Threshold)
                {
                    return true;
                }
            }
            return false;
        }

        public void RotateObject()
        {
            // Rota el objeto 90 grados en el eje Z
            rectTransform.Rotate(0, 0, 90);
            rotationState = (rotationState + 1) % 2; // Alternar entre 0 (sin rotar) y 1 (rotado 90 grados)
            Debug.Log("Rotando el objeto.");
        }

        private bool CanDrop()
        {
            RectTransform closestElement = null;
            float closestDistance = float.MaxValue;

            foreach (RectTransform uiElement in uiElementsToCompare)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(uiElement, Input.mousePosition, _canvas.worldCamera))
                {
                    Vector2 pivotPosition = rectTransform.TransformPoint(rectTransform.pivot);
                    Vector2 elementCenter = uiElement.TransformPoint(new Vector2(0.5f, 0.5f));

                    float distance = Vector2.Distance(pivotPosition, elementCenter);

                    if (distance < closestDistance)
                    {
                        closestDistance = distance;
                        closestElement = uiElement;
                    }
                }
            }

            return closestElement != null && closestDistance < Threshold;
        }
    }
}
