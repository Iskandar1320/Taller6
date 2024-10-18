using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TransitionsTimerManagerScript : MonoBehaviour
{
    [SerializeField] GameObject _blueShip;
    [SerializeField] GameObject _redShip;
    [SerializeField] GameObject _blueUI;
    [SerializeField] GameObject _redUI;
    [SerializeField] TextMeshProUGUI _winText;
    [SerializeField] GameObject _winPannel;
    [SerializeField] float _startTransitionsDelay = 4.0f;
    // Start is called before the first frame update
    void Start()
    {
        _winPannel.SetActive(true);
        _blueUI.SetActive(false);
        _redUI.SetActive(false);
        _blueShip.SetActive(false);
        _redShip.SetActive(false);
        StartCoroutine(StartGame());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private IEnumerator StartGame()
    {
        

        for (float timer = _startTransitionsDelay; timer > 0; timer -= Time.deltaTime)
        {
            _winText.text = "" + Mathf.Ceil(timer).ToString();  // Mostrar cuenta regresiva
            yield return null;
        }
        
        
        GameStart();
    }
    private void GameStart()
    {
        _winText.text = "";
        _winPannel.SetActive(false);
        _blueUI.SetActive(true);
        _redUI.SetActive(true);
        _blueShip.SetActive(true);
        _redShip.SetActive(true);
    }


}
