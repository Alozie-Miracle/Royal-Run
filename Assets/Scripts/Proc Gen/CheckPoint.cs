using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    GameManager gameManager;
    [SerializeField] float timeIncreaseAmount = 5f;
    const string playerTag = "Player";

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            gameManager.IncreaseTime(timeIncreaseAmount);
        }
    }
}
