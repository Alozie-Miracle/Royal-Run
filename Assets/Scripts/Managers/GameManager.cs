using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] TMP_Text timeText;
    [SerializeField] GameObject gameOverText;
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
        bool flowControl = FlowControl();
        if (!flowControl)
        {
            return;
        }
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
        gameOverText.SetActive(true);
        Time.timeScale = .1f; // Pause the game
    }

    void UpdateTimeText()
    {
        timeText.text = "Time: " + timeLeft.ToString("F1") + "s";
    }

    public void IncreaseTime(float amount)
    {
        timeLeft += amount;
    }
}


