using UnityEngine;

public class SpawnerPuntos : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float spawnRate = 2f;
    private float nextSpawn = 0f;
    public Transform spawnPoint;
    public float maxY;
    void Update()
    {
        if (Time.time > nextSpawn)
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
