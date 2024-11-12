using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InstructionsPanel : MonoBehaviour
{
    [SerializeField] private GameObject instructionsPanel; // El panel de instrucciones
    [SerializeField] private float delayBeforeShowing = 3f; // Delay de 3 segundos

    void Start()
    {
        instructionsPanel.SetActive(false); // Asegurarse de que el panel estÅEdesactivado al inicio
        StartCoroutine(ShowInstructionsPanelAfterDelay()); // Comienza el delay para mostrar el panel

    }

    // Corutina para mostrar el panel de instrucciones despuÈs de un delay
    private IEnumerator ShowInstructionsPanelAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeShowing); // Esperar el delay
        instructionsPanel.SetActive(true); // Mostrar el panel de instrucciones
        Time.timeScale = 0f; // Pausar el tiempo en el juego
    }

    // FunciÛn para reanudar el juego y ocultar el panel
    public void ResumeGame()
    {
        instructionsPanel.SetActive(false); // Ocultar el panel de instrucciones
        Time.timeScale = 1f; // Reanudar el tiempo en el juego
    }
}
