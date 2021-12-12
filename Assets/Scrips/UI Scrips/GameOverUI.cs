using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    public GameObject mainMenuButon, exitGameButton;
    public TextMeshProUGUI gameResultsText;
    public TextMeshProUGUI scoreText;
    int gameWon;
    int score;
    // Start is called before the first frame update
    void Start()
    {
        mainMenuButon.GetComponent<Button>().onClick.AddListener(MainMenuButtonPressed);
        exitGameButton.GetComponent<Button>().onClick.AddListener(ExitGameButtonPressed);
        LoadGameResults();

    }

    void LoadGameResults()
    {
        if (PlayerPrefs.GetInt("Game Saved") == 1)
        {
            gameWon = PlayerPrefs.GetInt("Game Won");
            score = PlayerPrefs.GetInt("Score");
        }
        else
        {
            gameWon = 0;
            score = 0;
        }

        bool gameResults = (gameWon == 1) ? true : false;
        if (gameResults)
        {
            gameResultsText.text = "You Won!";
        }
        else
        {
            gameResultsText.text = "You Lost!";
        }
        scoreText.text = "Final Score: " + score;
    }

    void MainMenuButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }

    void ExitGameButtonPressed()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
