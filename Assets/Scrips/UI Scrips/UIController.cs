using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIController : MonoBehaviour
{
    delegate void JumpButtonPressedDelegate();
    JumpButtonPressedDelegate jumpButtonPressedDelegate;

    delegate void PunchButtonPressedDelegate();
    PunchButtonPressedDelegate punchButtonPressedDelegate;

    public static int score;
    public TextMeshProUGUI scoreText;

    public static bool aButtonPressed;
    public static bool bButtonPressed;

    public KirbyController kirbyRef;
    // Start is called before the first frame update
    void Start()
    {
        jumpButtonPressedDelegate = kirbyRef.KirbyJump;
        punchButtonPressedDelegate = kirbyRef.KirbyPunch;
        score = 0;
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
}
