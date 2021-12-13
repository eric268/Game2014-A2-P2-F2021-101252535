//-------------------------Level1UIManager.cs-------------------------------------------
//----------------Author: Eric Galway 101252535------------------------------------
//----------------Date Last Modified: Dec 11 2021----------------------------------
//  The file containts the script used for updating the level 1 scene UI
//  Revision History : 1.2 Added background sound

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level1UIManager : MonoBehaviour
{
    //Delegates for A and B Buttons
    delegate void JumpButtonPressedDelegate();
    JumpButtonPressedDelegate jumpButtonPressedDelegate;
    delegate void PunchButtonPressedDelegate();
    PunchButtonPressedDelegate punchButtonPressedDelegate;

    public static int score;
    public TextMeshProUGUI scoreText;

    public static bool aButtonPressed;
    public static bool bButtonPressed;

    public GameObject[] heartArray;

    public int livesRemaining;

    public KirbyController kirbyRef;
    // Start is called before the first frame update
    void Start()
    {
        jumpButtonPressedDelegate = kirbyRef.KirbyJump;
        punchButtonPressedDelegate = kirbyRef.SwordAttack;
        score = 0;
        BackgroundSoundManager.PlaySound("Level1");
    }

    private void Update()
    {
        scoreText.text = score.ToString();
    }
    public void AButtonPressed()
    {
        jumpButtonPressedDelegate();
        aButtonPressed = true;

    }

    public void AButtonReleased()
    {
        aButtonPressed = false;
    }

    public void BButtonPressed()
    {
        punchButtonPressedDelegate();
        bButtonPressed = true;
    }

    public void BButtonReleased()
    {
        bButtonPressed = false;
    }

    public void UpdateHeartUI()
    {
        livesRemaining--;
        heartArray[livesRemaining].SetActive(false);
    }
}
