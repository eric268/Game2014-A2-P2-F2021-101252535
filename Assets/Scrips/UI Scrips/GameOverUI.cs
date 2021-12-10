using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameOverUI : MonoBehaviour
{
    public GameObject mainMenuButon, exitGameButton;
    // Start is called before the first frame update
    void Start()
    {
        mainMenuButon.GetComponent<Button>().onClick.AddListener(MainMenuButtonPressed);
        exitGameButton.GetComponent<Button>().onClick.AddListener(ExitGameButtonPressed);
    }

    // Update is called once per frame
    void Update()
    {
        
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
