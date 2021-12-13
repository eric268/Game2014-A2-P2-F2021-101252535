//-------------------------MainMenuUIManager.cs-------------------------------------------
//----------------Author: Eric Galway 101252535------------------------------------
//----------------Date Last Modified: Dec 11 2021----------------------------------
//  The file containts the script used for updating the main menu scene UI
//  Revision History : 1.2 Added background sound

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

//Main Menu UI manager class
public class MainMenuUIManager : MonoBehaviour
{
    public GameObject playButton, instructionsButton, exitButton;
    // Start is called before the first frame update
    void Start()
    {
        playButton.GetComponent<Button>().onClick.AddListener(PlayButtonPressed);
        instructionsButton.GetComponent<Button>().onClick.AddListener(InstructionsButtonPressed);
        exitButton.GetComponent<Button>().onClick.AddListener(ExitButtonPressed);
        BackgroundSoundManager.PlaySound("MainMenu");
    }
    //Play game
    void PlayButtonPressed()
    {
        SceneManager.LoadScene("Level1");
    }
    //Load instructions
    void InstructionsButtonPressed()
    {
        SceneManager.LoadScene("Instructions");
    }

    //Quit game
    void ExitButtonPressed()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }
}
