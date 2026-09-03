using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] ScoreManager scoreManager;
    [SerializeField] TMP_Text timeText;
    [SerializeField] GameObject gameOverText;
    [SerializeField] GameObject gameOverHighScoreText;
    [SerializeField] GameObject restartButton;
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
        Time.timeScale = 1f; // Ensure game runs at normal speed
        timeLeft = startTime;

        if (gameOverText != null) gameOverText.SetActive(false);
        if (gameOverHighScoreText != null) gameOverHighScoreText.SetActive(false);
        if (restartButton != null) restartButton.SetActive(false);
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
                gameOverHighScoreText.GetComponent<TMP_Text>().text = "High Score: " + scoreManager.HighScore;
        }

        if (gameOverText != null) gameOverText.SetActive(true);
        if (gameOverHighScoreText != null) gameOverHighScoreText.SetActive(true);
        if (restartButton != null) restartButton.SetActive(true);

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


    public void RestartGame()
    {
        Time.timeScale = 1f; // Reset time scale to normal
        scoreManager.ResetScore(); // Reset the score
        gameOver = false; // Reset game over state
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
    }
}

