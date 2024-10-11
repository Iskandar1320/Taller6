using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


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

        //[SerializeField] private ContadorDeColisiones cont;
        //public GameObject tapText;

        public bool isSpawning = true;
        void Update()
        {
            if (Input.GetMouseButtonDown(0) && !gameStarted)
            {
                StartCoroutine(SpawnRoutine());
                gameStarted = true;
                //tapText.SetActive(false);
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

        /*public void StartSpawning()
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
        }*/
    }
}
