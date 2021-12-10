using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    public GameObject playButton, instructionsButton, exitButton;
    // Start is called before the first frame update
    void Start()
    {
        playButton.GetComponent<Button>().onClick.AddListener(PlayButtonPressed);
        instructionsButton.GetComponent<Button>().onClick.AddListener(InstructionsButtonPressed);
        exitButton.GetComponent<Button>().onClick.AddListener(ExitButtonPressed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void PlayButtonPressed()
    {
        SceneManager.LoadScene("Level1");
    }
    void InstructionsButtonPressed()
    {
        SceneManager.LoadScene("Instructions");
    }
    void ExitButtonPressed()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
