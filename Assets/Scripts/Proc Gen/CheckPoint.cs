using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    GameManager gameManager;
    ObstacleSpanner obstacleSpanner;
    [SerializeField] float timeIncreaseAmount = 5f;
    [SerializeField] float obstacleSpawnTimeDecreaseAmount = 0.2f;
   
    const string playerTag = "Player";

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        obstacleSpanner = FindAnyObjectByType<ObstacleSpanner>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            gameManager.IncreaseTime(timeIncreaseAmount);
            obstacleSpanner.DecreaseObstacleSpawnTime(obstacleSpawnTimeDecreaseAmount);
        }
    }
}
