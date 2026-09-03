using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] TMP_Text timeText;
    [SerializeField] GameObject gameOverText;
    [SerializeField] GameObject gameOverHighScoreText;
    [SerializeField] TMP_Text gameOverScoreText;
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] float startTime = 5f;

    float timeLeft;
    bool gameOver = false;


    // public bool GameOver { get { return gameOver; } // set { gameOver = value; } }
    // public bool GameOver { get, private set; } // This is a property with a private setter
    // public bool GameOver { get => gameOver; }
    public bool GameOver => gameOver; // This is a read-only property

    void Start()
    {
        timeLeft = startTime;
    }

    void Update()
    {
        if (!FlowControl()) return;
    }

    bool FlowControl()
    {
        if (gameOver) return false;

        timeLeft -= Time.deltaTime;
        UpdateTimeText();

        if (timeLeft <= 0)
        {
            PlayerGameOver();
        }

        return true;
    }

    void PlayerGameOver()
    {
        gameOver = true;
        playerController.enabled = false;
        timeLeft = 0;

        // Display current score and high score on Game Over screen
        if (scoreManager != null)
        {
            if (gameOverScoreText != null)
                gameOverScoreText.text = "Final Score: " + scoreManager.Score;

            if (gameOverHighScoreText != null)
                highScoreText.text = "High Score: " + scoreManager.HighScore;
        }

        if (gameOverText != null)
        {
            gameOverText.SetActive(true);
            gameOverHighScoreText.SetActive(true);
        }

        Time.timeScale = 0.1f; // Pause game speed
    }

    void UpdateTimeText()
    {
        if (timeText != null)
        {
            timeText.text = "Time: " + timeLeft.ToString("F1") + "s";
        }
    }

    public void IncreaseTime(float amount)
    {
        timeLeft += amount;
    }
}

