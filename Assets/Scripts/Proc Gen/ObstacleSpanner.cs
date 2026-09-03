using System.Collections;
using UnityEngine;

public class ObstacleSpanner : MonoBehaviour
{
    [SerializeField] GameObject[] obstaclePrefabs;
    [SerializeField] Transform obstacleParent;
    [SerializeField] float obstacleSpawnTime = 2f;
    [SerializeField] float spawnWidth = 4f;
    [SerializeField] float DropChance = .5f;
    [SerializeField] float minObstacleSpawnTime = 0.2f;
    

    void Start()
    {
        StartCoroutine(SpawnObstacleRoutine());
    }

    public void DecreaseObstacleSpawnTime(float amount)
    {
        obstacleSpawnTime -= amount;
        obstacleSpawnTime = Mathf.Max(obstacleSpawnTime, minObstacleSpawnTime);
    }
    

    IEnumerator SpawnObstacleRoutine()
    {
        while (true)
        {
            if (Random.value < DropChance)
            {
                yield return new WaitForSeconds(obstacleSpawnTime);
            }
            
            GameObject obstaclePrefab = obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
            Vector3 spawnPosition = new Vector3(Random.Range(-spawnWidth, spawnWidth), transform.position.y, transform.position.z);

            yield return new WaitForSeconds(obstacleSpawnTime);

            Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity, obstacleParent);
        }

    }
}
