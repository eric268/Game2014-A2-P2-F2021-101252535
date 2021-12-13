//-------------------------GameOverUI.cs-------------------------------------------
//----------------Author: Eric Galway 101252535------------------------------------
//----------------Date Last Modified: Dec 11 2021----------------------------------
//  The file containts the script used for updating the game over scene UI
//  Revision History : 1.2 Added loading score and game won data

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
        BackgroundSoundManager.PlaySound("GameOver");
    }
    //Loads the saved game data
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
    //Main menu UI button 
    void MainMenuButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
    //Quit game button 
    void ExitGameButtonPressed()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
