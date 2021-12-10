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

    // Update is called once per frame
    void Update()
    {

    }

    void ResumeGameButtonPressed()
    {
        gameIsPaused = false;
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1.0f;
    }
    void MainMenuButtonPressed()
    {
        Time.timeScale = 1.0f;
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
    void PauseMenuButtonPressed()
    {
        Time.timeScale = 0.0f;
        gameIsPaused = true;
        pauseMenuCanvas.SetActive(true);
    }
}
