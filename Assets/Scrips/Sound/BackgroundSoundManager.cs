//-------------------------BackgroundSoundManager.cs--------------------------------------
//----------------Author: Eric Galway 101252535------------------------------------
//----------------Date Last Modified: Dec 11 2021----------------------------------
//  The file containts the script used for playing background sounds
//  Revision History : 1.0 Added static function and variables

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSoundManager : MonoBehaviour
{
    public static AudioClip mainMenuBackgroundSound, level1BackgroundSound, instructionsBackgroundSound, gameOverBackgroundSound;
    static  AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        mainMenuBackgroundSound = Resources.Load<AudioClip>("BackgroundSound/MainMenuBackgroundMusic");
        level1BackgroundSound = Resources.Load<AudioClip>("BackgroundSound/MainLevelBackgroundMusic");
        instructionsBackgroundSound = Resources.Load<AudioClip>("BackgroundSound/InstructionsBackgroundMusic");
        gameOverBackgroundSound = Resources.Load<AudioClip>("BackgroundSound/GameOverBackgroundMusic");

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //A static function that can play sound clips
    public static void PlaySound(string clip)
    {
        switch (clip)
        {
            case "MainMenu":
                audioSource.PlayOneShot(mainMenuBackgroundSound, 0.05f);
                break;
            case "Level1":
                audioSource.PlayOneShot(level1BackgroundSound, 0.05f);
                break;
            case "Instructions":
                audioSource.PlayOneShot(instructionsBackgroundSound, 0.05f);
                break;
            case "GameOver":
                audioSource.PlayOneShot(gameOverBackgroundSound, 0.05f);
                break;
        }
    }
}
