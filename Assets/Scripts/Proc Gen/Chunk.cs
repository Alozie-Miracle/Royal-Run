using System.Collections.Generic;
using UnityEngine;

public class Chunk : MonoBehaviour
{
    [SerializeField] GameObject fencePrefab;
    [SerializeField] GameObject applePrefab;
    [SerializeField] GameObject coinPrefab;
    [SerializeField] float[] lanes = {-2.5f, 0f, 2.5f};
    [SerializeField] float appleSpawnChance = .3f;
    [SerializeField] float coinSpawnChance = .5f;
    [SerializeField] float coinSeparationLength = 2f;

    List<int> availableLanes = new List<int> {0, 1, 2};

    void Start() {
        SpawnFences();
        SpawnApple();
        SpawnCoin();
    }

    void SpawnFences()
    {
        int fencesToSpawn = Random.Range(0, lanes.Length);

        for (int i = 0; i < fencesToSpawn; i++)
        {
            if (availableLanes.Count <= 0) break;
            int selectedLane = SelectLane();
            SpawnObejct(selectedLane, fencePrefab, transform.position.z);
        }
    }

   

    void SpawnApple()
    {
        if (Random.value > appleSpawnChance || availableLanes.Count <= 0) return;


        int selectedLane = SelectLane();
        SpawnObejct(selectedLane, applePrefab, transform.position.z);
    }

    void SpawnCoin()
    {
        if (Random.value > coinSpawnChance || availableLanes.Count <= 0) return;


        int selectedLane = SelectLane();
        int maxCoinToSpawn = 6;
        int coinToSpwan = Random.Range(0, maxCoinToSpawn);
        float topOfChunkZPos = transform.position.z + (coinSeparationLength * 2f);

        for (int i = 0; i < coinToSpwan; i++)
        {
            float spawnPositionZ = topOfChunkZPos - (i * coinSeparationLength);
            SpawnObejct(selectedLane, coinPrefab, spawnPositionZ);

        }

    }



    int SelectLane()
    {
        int randomLaneIndex = Random.Range(0, availableLanes.Count);
        int selectedLane = availableLanes[randomLaneIndex];
        availableLanes.RemoveAt(randomLaneIndex);

        return selectedLane;
    }

    void SpawnObejct(int selectedLane, GameObject gameObject, float zValue)
    {
        Vector3 spawnPosition = new Vector3(lanes[selectedLane], transform.position.y, zValue);
        Instantiate(gameObject, spawnPosition, Quaternion.identity, this.transform);
    }
}
