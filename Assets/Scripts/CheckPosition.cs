using UnityEngine;

public class PositionChecker : MonoBehaviour
{
    [field:SerializeField] public Vector3 _lastPosition { get; private set; }  // Almacena la última posición conocida
    private bool _positionChanged = false; // Estado de si la posición ha cambiado

    void Start()
    {
        // Inicializar la última posición con la posición actual al inicio
        _lastPosition = transform.position;
    }

    void Update()
    {
        // Comprobar si la posición ha cambiado
        CheckPositionChange();

        // Acción si la posición ha cambiado (ejemplo)
        if (_positionChanged)
        {
            Debug.Log("La posición ha cambiado.");
        }
    }

    public void CheckPositionChange()
    {
        // Comparar la posición actual con la última posición almacenada
        if (transform.position != _lastPosition)
        {
            _positionChanged = true; // Marcar que la posición ha cambiado
            _lastPosition = transform.position; // Actualizar la última posición conocida
        }
        else
        {
            _positionChanged = false; // No ha cambiado la posición
        }
    }

    // Función que puedes usar para obtener si la posición cambió
    public bool HasPositionChanged()
    {
        return _positionChanged;
    }
}
