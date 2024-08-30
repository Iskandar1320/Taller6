using UnityEngine;

public class ContadorDeColisiones : MonoBehaviour
{
    public int contador = 0;
    public int contAzul = 0;
    public int contRojo = 0;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PjroAma"))
        {
            contador++;
        }
    }
    public void Azul()
    {
        contAzul++;
        CompararContadores();
    }
    public void Rojo()
    {
        contRojo++;
        CompararContadores();
    }
    private void CompararContadores()
    {
        int diferencia1 = Mathf.Abs(contador - contAzul);
        int diferencia2 = Mathf.Abs(contador - contRojo);

        if (diferencia1 < diferencia2)
        {
            Debug.Log("Azul gana");
        }
        else if (diferencia2 < diferencia1)
        {
            Debug.Log("Rojo gana");
        }
        else
        {
            Debug.Log("Empate");
        }
    }
}
