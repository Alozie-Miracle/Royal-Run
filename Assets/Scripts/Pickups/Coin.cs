using UnityEngine;

public class Coin : PickUp
{
    [SerializeField] int scoreValue = 1;
    ScoreManager scoreManager;

    private void Start() {
        scoreManager = FindAnyObjectByType<ScoreManager>();
    }

    protected override void onPickUp()
    {
        scoreManager.AddScore(scoreValue);
    }
}
