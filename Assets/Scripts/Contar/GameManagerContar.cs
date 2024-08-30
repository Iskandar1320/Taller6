using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerContar : MonoBehaviour
{
    public GameObject PjroAma;
    public GameObject PjroVer;

    public float maxX;
    public Transform spawnPoint;
    public float spawnRate;

    bool gameStarted = false;

    public GameObject tapText;


    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !gameStarted)
        {
            StartSpawning();
            gameStarted = true;
            tapText.SetActive(false);
        }
    }

    private void StartSpawning()
    {
        InvokeRepeating("SpawnPjroAma", 0.2f, spawnRate);
        InvokeRepeating("SpawnPjroVer", 0.5f, spawnRate);
    }
    void SpawnPjroAma()
    {
        Vector3 spawnPos = spawnPoint.position;
        spawnPos.x = Random.Range(-maxX, maxX);
        Instantiate(PjroAma, spawnPos, Quaternion.identity);
    }
    void SpawnPjroVer()
    {
        Vector3 spawnPos = spawnPoint.position;
        spawnPos.x = Random.Range(-maxX, maxX);
        Instantiate(PjroVer, spawnPos, Quaternion.identity);
    }  

}
