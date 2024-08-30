using UnityEngine;

public class ContadorDeColisiones : MonoBehaviour
{
    public int contador = 0; 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PjroAma"))
        {
            contador++;
            Debug.Log("Contador incrementado: " + contador);
        }
    }
}
