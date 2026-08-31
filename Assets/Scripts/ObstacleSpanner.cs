using System.Collections;
using UnityEngine;

public class ObstacleSpanner : MonoBehaviour
{
    [SerializeField] GameObject obstaclePrefab;
    [SerializeField] Transform obstacleParent;
    [SerializeField] float obstacleSpawnTime = 1f;
    

    void Start()
    {
        StartCoroutine(SpawnObstacleRoutine());
    }

    IEnumerator SpawnObstacleRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(obstacleSpawnTime);

            Instantiate(obstaclePrefab, transform.position, Quaternion.identity, obstacleParent);
        }
    }
}
