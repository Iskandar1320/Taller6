using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerStartPong : MonoBehaviour
{
    private float tiempoIncial = 3;
    private float tiempoActual = 0;
    private TextMeshProUGUI textoTiempo;
    [SerializeField] private GameObject pelota;

    private void Start()
    {
        textoTiempo = GetComponentInChildren<TextMeshProUGUI>();
        tiempoActual = tiempoIncial;
    }
    private void Update()
    {
        StartCoroutine(EsperarTransicion());
    }
    private void IniciarCuenta()
    {
        tiempoActual -= 1 * Time.deltaTime;
        textoTiempo.text = tiempoActual.ToString("0");
        if (tiempoActual <= 0)
        {
            pelota.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    private IEnumerator EsperarTransicion()
    {
        yield return new WaitForSeconds(1.2f);
        IniciarCuenta();

    }
}
