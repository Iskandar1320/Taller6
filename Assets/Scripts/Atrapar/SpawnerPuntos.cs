using Unity.VisualScripting;
using UnityEngine;

public class SpawnerPuntos : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float spawnRate = 2f;
    private float nextSpawn = 0f;
    public Transform spawnPoint;
    public float maxY;
    public bool isSpawning = false;
    void Update()
    {
        if (Time.time > nextSpawn && isSpawning)
        {
            nextSpawn = Time.time + spawnRate;
            SpawnProjectile();
        }
    }

    void SpawnProjectile()
    {
        Vector3 spawnPosition = spawnPoint.position;
        spawnPosition.y = Random.Range(-maxY, maxY);
        Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);
    }
}
