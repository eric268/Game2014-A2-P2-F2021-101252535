//-------------------------PauseMenuUI.cs-------------------------------------------
//----------------Author: Eric Galway 101252535------------------------------------
//----------------Date Last Modified: Dec 11 2021----------------------------------
//  The file containts the script used for updating the pause menu scene UI
//  Revision History : 1.2 Added background sound

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    [SerializeField]
    GameObject resumeGameButton, mainMenuButton, exitGameButton, pauseMenuButton;

    [SerializeField]
    GameObject pauseMenuCanvas;

    bool gameIsPaused = false;
    // Start is called before the first frame update
    void Start()
    {
        resumeGameButton.GetComponent<Button>().onClick.AddListener(ResumeGameButtonPressed);
        mainMenuButton.GetComponent<Button>().onClick.AddListener(MainMenuButtonPressed);
        exitGameButton.GetComponent<Button>().onClick.AddListener(ExitGameButtonPressed);
        pauseMenuButton.GetComponent<Button>().onClick.AddListener(PauseMenuButtonPressed);

        pauseMenuCanvas.SetActive(false);
    }
    //Resumes game
    void ResumeGameButtonPressed()
    {
        gameIsPaused = false;
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1.0f;
    }
    //Main Menu
    void MainMenuButtonPressed()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainMenu");
    }
    //Quit game
    void ExitGameButtonPressed()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
    void PauseMenuButtonPressed()
    {
        Time.timeScale = 0.0f;
        gameIsPaused = true;
        pauseMenuCanvas.SetActive(true);
    }
}
