using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScoreManager : MonoBehaviour
{
    public int playerScore;
    public Text scoreText;
    public GameObject gameOverScreen;
    public PlayerScript player;

    // Add a new field for high score
    public Text highScoreText;
    private int highScore;

    void Start()
    {
        // Load the high score at the start
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = "High Score: " + highScore.ToString();
    }

    [ContextMenu("Increase Score")]
    public void addScore(int scoreToAdd)
    {
        if (player.playerIsAlive == true)
        {
            playerScore += scoreToAdd;
            scoreText.text = playerScore.ToString();

            // Check and update high score
            if (playerScore > highScore)
            {
                highScore = playerScore;
                highScoreText.text = "High Score: " + highScore.ToString();
                PlayerPrefs.SetInt("HighScore", highScore);
                PlayerPrefs.Save();
            }
        }
    }

    public void restartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void gameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void quitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
