using System.Collections;
using UnityEngine;

namespace Contar
{
    public class GameManagerContar : MonoBehaviour
    {
        public GameObject PjroAma;
        public GameObject PjroVer;

        public float maxX;
        public Transform spawnPoint;
        public float spawnRatePjroAma;
        public float spawnRatePjroVer;
        public bool gameStarted = false;
        public bool isSpawning = true;

        private void Start()
        {
            ReiniciarSpawnRates();
        }
        void Update()
        {
            if (Input.GetMouseButtonDown(0) && !gameStarted)
            {
                StartCoroutine(SpawnRoutine());
                gameStarted = true;
            }
        }

        IEnumerator SpawnRoutine()
        {
            while (true)
            {
                if (isSpawning)
                {
                    SpawnPrefabPjroAma();
                }
                yield return new WaitForSeconds(spawnRatePjroAma);
                if (isSpawning)
                {
                    SpawnPrefabPjroVer();
                }
                yield return new WaitForSeconds(spawnRatePjroVer);
            }
        }
        void SpawnPrefabPjroAma()
        {
            Vector3 spawnPos = spawnPoint.position;
            spawnPos.x = Random.Range(-maxX, maxX);
            Instantiate(PjroAma, spawnPos, Quaternion.identity);
        }
        void SpawnPrefabPjroVer()
        {
            Vector3 spawnPos = spawnPoint.position;
            spawnPos.x = Random.Range(-maxX, maxX);
            Instantiate(PjroVer, spawnPos, Quaternion.identity);
        }

        public void ReiniciarSpawnRates()
        {
            spawnRatePjroAma = Random.Range(0.1f, 0.15f);
            spawnRatePjroVer = Random.Range(0.3f, 0.35f);
        }
    }
}
