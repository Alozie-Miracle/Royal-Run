using System.Collections;
using UnityEngine;

public class ObstacleSpanner : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] Transform obstacleParent;
    [SerializeField] float obstacleSpawnTime = 2f;
    [SerializeField] float spawnWidth = 4f;
    

    void Start()
    {
        StartCoroutine(SpawnObstacleRoutine());
    }

    IEnumerator SpawnObstacleRoutine()
    {
        while (true)
        {
            GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnWidth, spawnWidth), transform.position.y, transform.position.z);

            yield return new WaitForSeconds(obstacleSpawnTime);

            Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity, obstacleParent);
        }

    }
}
