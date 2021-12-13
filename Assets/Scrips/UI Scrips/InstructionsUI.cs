//-------------------------InstructionsUI.cs-------------------------------------------
//----------------Author: Eric Galway 101252535------------------------------------
//----------------Date Last Modified: Dec 11 2021----------------------------------
//  The file containts the script used for updating the instructions scene UI
//  Revision History : 1.2 Added background sound

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InstructionsUI : MonoBehaviour
{
    [SerializeField]
    GameObject mainMenuButton, nextPage1Button, nextPage2Button, previousPage1Button, previousPage2Button;

    [SerializeField]
    GameObject goalCanvas, platformsCanvas, enemiesCanvas;
    // Start is called before the first frame update
    void Start()
    {
        platformsCanvas.SetActive(false);
        enemiesCanvas.SetActive(false);

        mainMenuButton.GetComponent<Button>().onClick.AddListener(MainMenuButtonPressed);
        nextPage1Button.GetComponent<Button>().onClick.AddListener(NextPage1ButtonPressed);
        nextPage2Button.GetComponent<Button>().onClick.AddListener(NextPage2MenuButtonPressed);
        previousPage1Button.GetComponent<Button>().onClick.AddListener(PreviousPage1ButtonPressed);
        previousPage2Button.GetComponent<Button>().onClick.AddListener(PreviousPage2ButtonPressed);

        BackgroundSoundManager.PlaySound("Instructions");
    }

    void MainMenuButtonPressed()
    {
        SceneManager.LoadScene("MainMenu");
    }
    void NextPage1ButtonPressed()
    {
        ShowCorrectCanvas(platformsCanvas);
    }
    void NextPage2MenuButtonPressed()
    {
        ShowCorrectCanvas(enemiesCanvas);
    }
    void PreviousPage1ButtonPressed()
    {
        ShowCorrectCanvas(goalCanvas);
    }
    void PreviousPage2ButtonPressed()
    {
        ShowCorrectCanvas(platformsCanvas);
    }
    //Ensures that only one canvas is shown at a time
    void ShowCorrectCanvas(GameObject canvas)
    {
        goalCanvas.SetActive(false);
        platformsCanvas.SetActive(false);
        enemiesCanvas.SetActive(false);
        canvas.SetActive(true);
    }
}
